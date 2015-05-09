using InMemoryGatewayDemo.Entities;

namespace InMemoryGatewayDemo.Gateways
{
    public interface IEquipmentGateway
    {
        bool HasEquipmentForEmployee(long employeeId);

        Equipment GetEquipmentForEmployee(long employeeId);
    }
}
