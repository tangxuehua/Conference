using System;
using System.Collections.Generic;
using System.Linq;
using ENode.Domain;
using Registration.Orders;

namespace Registration.SeatAssigning
{
    [Serializable]
    public class SeatAssignment
    {
        public int Position { get; private set; }
        public Guid SeatType { get; private set; }
        public Attendee Attendee { get; set; }

        public SeatAssignment(int position, Guid seatType)
        {
            Position = position;
            SeatType = seatType;
        }
    }
}
