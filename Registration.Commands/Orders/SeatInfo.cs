using System;

namespace Registration.Commands.Orders
{
    [Serializable]
    public class SeatInfo
    {
        public Guid SeatTypeId { get; set; }
        public string SeatTypeName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
