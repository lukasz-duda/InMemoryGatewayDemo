using InMemoryGatewayDemo.Entities;
using InMemoryGatewayDemo.Gateways;
using System.Collections.Generic;

namespace InMemoryGatewayDemo.UseCases
{
    public class CheckAvailabilityOfEquipmentUseCase
    {
        public IEmployeeGateway EmployeeGateway { get; set; }

        public IDateTimeProvider DateTime { get; set; }

        public IEquipmentGateway EquipmentGateway { get; set; }

        public IStockGateway StockGateway { get; set; }

        public void Execute(CheckAvailabilityOfEquipmentRequest request)
        {
            IList<Employee> employeesInSector = EmployeeGateway.FindEmployeesBySector(request.SectorId);
            foreach (Employee employee in employeesInSector)
            {
                CheckEmployeeEquipment(employeesInSector[0]);
            }
        }

        private void CheckEmployeeEquipment(Employee employee)
        {
            if (employee.ScheduledToWork(DateTime.Now()))
            {
                if (EquipmentGateway.HasEquipmentForEmployee(employee.Id))
                {
                    Equipment equipment = EquipmentGateway.GetEquipmentForEmployee(employee.Id);
                    StockGateway.SendRequestEquipmentMessage(employee.Id, equipment.Id);
                }
                else
                {
                    StockGateway.SendNoEquipmentWarning(employee.Id);
                }
            }
        }
    }
}
