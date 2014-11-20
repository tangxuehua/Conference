using System;

namespace Registration.Commands.Orders
{
    [Serializable]
    public class SeatInfo
    {
        public Guid SeatType { get; set; }
        public int Quantity { get; set; }
    }
}
