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
            sector.Id = 35;

            stockGateway = new StockGatewaySpy();
            useCase.StockGateway = (IStockGateway)stockGateway;
        }

        [Test]
        public void WithScheduledEmployeeWithoutEquipment_SendsWarningToStock()
        {
            var employee = new Employee();
            employee.Sector = sector;
            employee.ScheduleWork(now);
            employeeGateway.Save(employee);

            Execute();

            Assert.True(stockGateway.SentNoEquipmentWarning);
            Assert.AreEqual(employee.Id, stockGateway.ReportedEmployeeWithoutEquipmentId);
        }

        public void Execute()
        {
            var request = new CheckAvailabilityOfEquipmentRequest();
            request.SectorId = sector.Id;
            useCase.Execute(request);
        }

        [Test]
        public void WithNotScheduledEmployee_DoesntSendWarningToStock()
        {
            var employee = new Employee();
            employee.Sector = sector;
            employeeGateway.Save(employee);

            Execute();

            Assert.False(stockGateway.SentNoEquipmentWarning);
        }

        [Test]
        public void WithScheduledEmployeeFromDifferentSector_DoesntSendWarningToStock()
        {
            Sector differentSector = new Sector();
            differentSector.Id = 55;
            var employee = new Employee();
            employee.Sector = differentSector;
            employee.ScheduleWork(now);
            employeeGateway.Save(employee);

            Execute();

            Assert.False(stockGateway.SentNoEquipmentWarning);
        }
    }
}
