using System;
using ENode.Eventing;
using Registration.Orders;

namespace Registration.SeatAssigning
{
    [Serializable]
    public class SeatAssigned : DomainEvent<Guid>
    {
        public int Position { get; private set; }
        public SeatType Seat { get; private set; }
        public Attendee Attendee { get; private set; }

        public SeatAssigned() { }
        public SeatAssigned(SeatAssignments seatAssignments, int position, SeatType seat, Attendee attendee)
            : base(seatAssignments)
        {
            Position = position;
            Seat = seat;
            Attendee = attendee;
        }
    }
}
