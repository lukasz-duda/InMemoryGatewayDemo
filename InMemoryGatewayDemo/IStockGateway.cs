namespace InMemoryGatewayDemo
{
    public interface IStockGateway
    {
        void SendRequestEquipmentMessage(long employeeId, long equipmentId);

        void SendNoEquipmentWarning(long employeeId);
    }
}
