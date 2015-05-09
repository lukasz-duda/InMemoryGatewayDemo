namespace InMemoryGatewayDemo
{
    public class CheckAvailabilityOfEquipmentUseCase
    {
        public IEmployeeGateway EmployeeGateway { get; set; }

        public IDateTimeProvider DateTime { get; set; }

        public IEquipmentGateway EquipmentGateway { get; set; }

        public IStockGateway StockGateway { get; set; }

        public void Execute(CheckAvailabilityOfEquipmentRequest request)
        {
            foreach (Employee employee in EmployeeGateway.FindEmployeesBySector(request.SectorId))
            {
                if (employee.ScheduledToWork(DateTime.Now()))
                {
                    if (EquipmentGateway.HasEquipmentForEmployee(employee.Id))
                    {
                        Equipment equipment = EquipmentGateway.GetEquipmentForEmployee(employee.Id);
                        StockGateway.SendRequestEquipmentMessage(employee.Id, equipment.Id);
                    }
                    StockGateway.SendNoEquipmentWarning(employee.Id);
                }
            }
        }
    }
}
