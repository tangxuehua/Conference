using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace Registration.SeatAssigning
{
    [Serializable]
    [Code(3307)]
    public class SeatAssigned : DomainEvent<Guid>
    {
        public int Position { get; private set; }
        public SeatType Seat { get; private set; }
        public Attendee Attendee { get; private set; }

        public SeatAssigned() { }
        public SeatAssigned(OrderSeatAssignments source, int position, SeatType seat, Attendee attendee) : base(source)
        {
            Position = position;
            Seat = seat;
            Attendee = attendee;
        }
    }
}
