using System;

namespace InMemoryGatewayDemo
{
    public class StockGatewaySpy : IStockGateway
    {
        public void SendRequestEquipmentMessage(long employeeId, long equipmentId)
        {
            throw new NotImplementedException();
        }

        public void SendNoEquipmentWarning(long employeeId)
        {
            SentNoEquipmentWarning = true;
            ReportedEmployeeWithoutEquipmentId = employeeId;
        }

        public bool SentNoEquipmentWarning { get; private set; }

        public long ReportedEmployeeWithoutEquipmentId { get; private set; }
    }
}
