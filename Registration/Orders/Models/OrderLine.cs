using System;

namespace Registration.Orders
{
    [Serializable]
    public class OrderLine
    {
        public SeatInfo SeatInfo { get; private set; }
        public decimal LineTotal { get; private set; }

        public OrderLine(SeatInfo seatInfo, decimal lineTotal)
        {
            SeatInfo = seatInfo;
            LineTotal = lineTotal;
        }
    }
}
