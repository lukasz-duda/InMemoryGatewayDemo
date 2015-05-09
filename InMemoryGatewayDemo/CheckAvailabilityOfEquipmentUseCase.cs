namespace InMemoryGatewayDemo
{
    public class CheckAvailabilityOfEquipmentUseCase
    {
        public IDateTimeProvider DateTime { get; set; }

        public IEmployeeGateway EmployeeGateway { get; set; }

        public IStockGateway StockGateway { get; set; }

        public void Execute(CheckAvailabilityOfEquipmentRequest request)
        {
            foreach (Employee employee in EmployeeGateway.FindEmployeesBySector(request.SectorId))
            {
                if (employee.ScheduledToWork(DateTime.Now()))
                {
                    StockGateway.SendNoEquipmentWarning(employee.Id);
                }
            }
        }
    }
}
