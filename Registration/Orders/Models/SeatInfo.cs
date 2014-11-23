using System;

namespace Registration
{
    [Serializable]
    public class SeatInfo
    {
        public Guid SeatTypeId { get; private set; }
        public string SeatTypeName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        public SeatInfo(Guid seatTypeId, string seatTypeName, decimal unitPrice, int quantity)
        {
            SeatTypeId = seatTypeId;
            SeatTypeName = seatTypeName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
