namespace InMemoryGatewayDemo
{
    public interface IEquipmentGateway
    {
        bool HasEquipmentForEmployee(long employeeId);

        Equipment GetEquipmentForEmployee(long employeeId);
    }
}
