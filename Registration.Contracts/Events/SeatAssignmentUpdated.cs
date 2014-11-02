using System;
using ENode.Eventing;

namespace Registration.Events
{
    public class SeatAssignmentUpdated : DomainEvent<Guid>
    {
        public int Position { get; set; }
        public PersonalInfo Attendee { get; set; }

        public SeatAssignmentUpdated(Guid sourceId) : base(sourceId) { }
    }
}
