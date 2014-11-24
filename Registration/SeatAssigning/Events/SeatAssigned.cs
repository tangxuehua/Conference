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

        public SeatAssigned(Guid assignmentsId, int position, SeatType seat, Attendee attendee) : base(assignmentsId)
        {
            Position = position;
            Seat = seat;
            Attendee = attendee;
        }
    }
}
