using NUnit.Framework;

namespace InMemoryGatewayDemo
{
    [TestFixture]
    public class CheckAvailabilityOfEquipmentUseCaseTest
    {
        private CheckAvailabilityOfEquipmentUseCase useCase;

        [SetUp]
        public void SetUpTestMethod()
        {
            useCase = new CheckAvailabilityOfEquipmentUseCase();
        }
    }
}
