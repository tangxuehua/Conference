using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.SeatAssigning
{
    [Serializable]
    public class SeatAssignmentsCreated : DomainEvent<Guid>
    {
        public IEnumerable<SeatAssignment> Assignments { get; private set; }

        public SeatAssignmentsCreated(SeatAssignments seatAssignments, IEnumerable<SeatAssignment> details)
            : base(seatAssignments)
        {
            Assignments = details;
        }
    }
}
