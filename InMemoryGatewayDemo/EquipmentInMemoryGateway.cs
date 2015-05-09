using System.Linq;

namespace InMemoryGatewayDemo
{
    public class EquipmentInMemoryGateway : InMemoryGateway<Equipment>, IEquipmentGateway
    {
        public bool HasEquipmentForEmployee(long employeeId)
        {
            return GetAll().Any(equipment => equipment.Employee.Id == employeeId);
        }

        public Equipment GetEquipmentForEmployee(long employeeId)
        {
            return GetAll().Single(equipment => equipment.Employee.Id == employeeId);
        }
    }
}
