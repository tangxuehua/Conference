using System;

namespace Registration
{
    public class OrderLine
    {
        public decimal LineTotal { get; set; }
    }

    public class SeatOrderLine : OrderLine
    {
        public Guid SeatType { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}