using System;
using ENode.Eventing;

namespace Registration.Events
{
    public class SeatAssigned : DomainEvent<Guid>
    {
        public int Position { get; set; }
        public Guid SeatType { get; set; }
        public PersonalInfo Attendee { get; set; }

        public SeatAssigned(Guid sourceId) : base(sourceId) { }
    }
}
