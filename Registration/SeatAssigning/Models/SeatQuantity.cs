using System;

namespace Registration.SeatAssigning
{
    [Serializable]
    public class SeatQuantity
    {
        public Guid SeatType { get; private set; }
        public int Quantity { get; private set; }

        public SeatQuantity(Guid seatType, int quantity)
        {
            SeatType = seatType;
            Quantity = quantity;
        }
    }
}
