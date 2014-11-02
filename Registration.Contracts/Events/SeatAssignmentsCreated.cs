using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.Events
{
    public class SeatAssignmentsCreated : DomainEvent<Guid>
    {
        public class SeatAssignmentInfo
        {
            public int Position { get; set; }
            public Guid SeatType { get; set; }
        }

        public Guid OrderId { get; set; }
        public IEnumerable<SeatAssignmentInfo> Seats { get; set; }

        public SeatAssignmentsCreated(Guid sourceId) : base(sourceId) { }
    }
}
