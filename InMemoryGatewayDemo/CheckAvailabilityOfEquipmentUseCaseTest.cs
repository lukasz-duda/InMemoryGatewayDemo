using NUnit.Framework;
using System;

namespace InMemoryGatewayDemo
{
    public class CheckAvailabilityOfEquipmentUseCaseTest
    {
        protected DateTime now;
        protected CheckAvailabilityOfEquipmentUseCase useCase;
        protected DateTimeProviderStub dateTimeProvider;
        protected EmployeeInMemoryGateway employeeGateway;
        protected Sector sector;
        protected StockGatewaySpy stockGateway;

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

        public void Execute()
        {
            var request = new CheckAvailabilityOfEquipmentRequest();
            request.SectorId = sector.Id;
            useCase.Execute(request);
        }

        [TestFixture]
        public class GivenScheduledEmployeeInSectorWithoutEquipment : CheckAvailabilityOfEquipmentUseCaseTest
        {
            protected Employee employee;

            [SetUp]
            public override void SetUpTestMethod()
            {
                base.SetUpTestMethod();
                employee = new Employee();
                employee.ScheduleWork(now);
                employee.Sector = sector;
                employeeGateway.Save(employee);
            }

            [Test]
            public void SendsWarningToStock()
            {
                Execute();

                Assert.True(stockGateway.SentNoEquipmentWarning);
                Assert.AreEqual(employee.Id, stockGateway.ReportedEmployeeWithoutEquipmentId);
            }
        }
    }
}
