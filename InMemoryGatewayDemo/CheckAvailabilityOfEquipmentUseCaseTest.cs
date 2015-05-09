using NUnit.Framework;
using System;

namespace InMemoryGatewayDemo
{
    [TestFixture]
    public class CheckAvailabilityOfEquipmentUseCaseTest
    {
        private DateTime now;
        private CheckAvailabilityOfEquipmentUseCase useCase;
        private DateTimeProviderStub dateTimeProvider;
        private EmployeeInMemoryGateway employeeGateway;
        private Sector sector;
        private StockGatewaySpy stockGateway;
        private EquipmentInMemoryGateway equipmentGateway;

        [SetUp]
        public virtual void SetUpTestMethod()
        {
            useCase = new CheckAvailabilityOfEquipmentUseCase();

            now = new DateTime(2001, 2, 3, 4, 5, 6, DateTimeKind.Utc);
            dateTimeProvider = new DateTimeProviderStub(now);
            useCase.DateTime = (IDateTimeProvider)dateTimeProvider;

            employeeGateway = new EmployeeInMemoryGateway();
            useCase.EmployeeGateway = (IEmployeeGateway)employeeGateway;

            sector = new Sector();
            sector.Id = 1;

            stockGateway = new StockGatewaySpy();
            useCase.StockGateway = (IStockGateway)stockGateway;

            equipmentGateway = new EquipmentInMemoryGateway();
            useCase.EquipmentGateway = (IEquipmentGateway)equipmentGateway;
        }

        [Test]
        public void WithScheduledEmployeeWithoutEquipment_SendsWarningToStock()
        {
            Employee employee = MakeScheduledEmployee();
            employeeGateway.Save(employee);

            Execute();

            Assert.True(stockGateway.SentNoEquipmentWarning);
            Assert.AreEqual(employee.Id, stockGateway.ReportedEmployeeWithoutEquipmentId);
        }

        private Employee MakeScheduledEmployee()
        {
            var employee = new Employee();
            employee.Sector = sector;
            employee.ScheduleWork(now);
            return employee;
        }

        public void Execute()
        {
            var request = new CheckAvailabilityOfEquipmentRequest();
            request.SectorId = sector.Id;
            useCase.Execute(request);
        }

        [Test]
        public void WithNotScheduledEmployee_DoesntSendAnyMessageToStock()
        {
            var employee = new Employee();
            employee.Sector = sector;
            employeeGateway.Save(employee);

            Execute();

            Assert.False(stockGateway.SentNoEquipmentWarning);
            Assert.False(stockGateway.SentRequestEquipmentMessage);
        }

        [Test]
        public void WithScheduledEmployeeFromDifferentSector_DoesntAnyMessageToStock()
        {
            Sector differentSector = new Sector();
            differentSector.Id = 2;
            var employee = new Employee();
            employee.Sector = differentSector;
            employee.ScheduleWork(now);
            employeeGateway.Save(employee);

            Execute();

            Assert.False(stockGateway.SentNoEquipmentWarning);
            Assert.False(stockGateway.SentRequestEquipmentMessage);
        }

        [Test]
        public void WithEquipment_SendsRequestToStock()
        {
            Employee employee = MakeScheduledEmployee();
            employeeGateway.Save(employee);
            Equipment equipment = new Equipment();
            equipment.Employee = employee;
            equipmentGateway.Save(equipment);

            Execute();

            Assert.True(stockGateway.SentRequestEquipmentMessage);
            Assert.AreEqual(equipment.Id, stockGateway.RequestedEquipmentId);
            Assert.AreEqual(employee.Id, stockGateway.RequestedEquipmentEmployeeId);
        }
    }
}
