namespace InMemoryGatewayDemo
{
    public class StockGatewaySpy : IStockGateway
    {
        public void SendRequestEquipmentMessage(long employeeId, long equipmentId)
        {
            SentRequestEquipmentMessage = true;
            RequestedEquipmentEmployeeId = employeeId;
            RequestedEquipmentId = equipmentId;
        }

        public bool SentRequestEquipmentMessage { get; private set; }

        public long RequestedEquipmentId { get; private set; }

        public long RequestedEquipmentEmployeeId { get; private set; }

        public void SendNoEquipmentWarning(long employeeId)
        {
            SentNoEquipmentWarning = true;
            ReportedEmployeeWithoutEquipmentId = employeeId;
        }

        public bool SentNoEquipmentWarning { get; private set; }

        public long ReportedEmployeeWithoutEquipmentId { get; private set; }
    }
}
