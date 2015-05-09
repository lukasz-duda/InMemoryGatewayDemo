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
        private Employee employee;
        private Sector sector;
        private StockGatewaySpy stockGateway;
        private EquipmentInMemoryGateway equipmentGateway;
        private Equipment equipment;

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
            SetUpScheduledEmployee();

            Execute();

            Assert.True(stockGateway.SentNoEquipmentWarning);
            Assert.AreEqual(employee.Id, stockGateway.ReportedEmployeeWithoutEquipmentId);
        }

        private void SetUpScheduledEmployee()
        {
            employee = new Employee();
            employee.Sector = sector;
            employee.ScheduleWork(now);
            employeeGateway.Save(employee);
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
            SetUpNotScheduledEmployee();

            Execute();

            Assert.False(stockGateway.SentNoEquipmentWarning);
            Assert.False(stockGateway.SentRequestEquipmentMessage);
        }

        private void SetUpNotScheduledEmployee()
        {
            employee = new Employee();
            employee.Sector = sector;
            employeeGateway.Save(employee);
        }

        [Test]
        public void WithEmployeeScheduledForDifferentDay_DoesntSendAnyMessageToStock()
        {
            SetUpNotScheduledEmployee();
            DateTime differentDay = now.AddDays(1);
            employee.ScheduleWork(differentDay);

            Execute();

            Assert.False(stockGateway.SentNoEquipmentWarning);
            Assert.False(stockGateway.SentRequestEquipmentMessage);
        }

        [Test]
        public void WithScheduledEmployeeFromDifferentSector_DoesntAnyMessageToStock()
        {
            SetUpEmployeeFromDifferentSector();

            Execute();

            Assert.False(stockGateway.SentNoEquipmentWarning);
            Assert.False(stockGateway.SentRequestEquipmentMessage);
        }

        private void SetUpEmployeeFromDifferentSector()
        {
            Sector differentSector = new Sector();
            differentSector.Id = 2;
            var employee = new Employee();
            employee.Sector = differentSector;
            employee.ScheduleWork(now);
            employeeGateway.Save(employee);
        }

        [Test]
        public void WithEquipment_SendsRequestToStock()
        {
            SetUpScheduledEmployeeWithEquipment();

            Execute();

            Assert.True(stockGateway.SentRequestEquipmentMessage);
            Assert.AreEqual(equipment.Id, stockGateway.RequestedEquipmentId);
            Assert.AreEqual(employee.Id, stockGateway.RequestedEquipmentEmployeeId);
        }

        [Test]
        public void WithEquipment_DoesntSendNoEquipmentWarning()
        {
            SetUpScheduledEmployeeWithEquipment();

            Execute();

            Assert.False(stockGateway.SentNoEquipmentWarning);
        }

        private void SetUpScheduledEmployeeWithEquipment()
        {
            SetUpScheduledEmployee();

            equipment = new Equipment();
            equipment.Employee = employee;
            equipmentGateway.Save(equipment);
        }
    }
}
