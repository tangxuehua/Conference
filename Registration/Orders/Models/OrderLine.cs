using System;

namespace Registration.Orders
{
    [Serializable]
    public class OrderLine
    {
        public Guid SeatType { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }
}
