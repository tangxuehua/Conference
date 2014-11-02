using System;

namespace Registration
{
    public class OrderItem
    {
        public OrderItem(Guid seatType, int quantity)
        {
            this.SeatType = seatType;
            this.Quantity = quantity;
        }

        public Guid SeatType { get; private set; }
        public int Quantity { get; private set; }
    }
}
