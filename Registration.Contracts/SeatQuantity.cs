using System;

namespace Registration
{
    public class SeatQuantity
    {
        public SeatQuantity(Guid seatType, int quantity)
        {
            SeatType = seatType;
            Quantity = quantity;
        }

        public Guid SeatType { get; private set; }
        public int Quantity { get; private set; }
    }
}
