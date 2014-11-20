using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.SeatAssigning
{
    [Serializable]
    public class SeatAssignmentsCreated : DomainEvent<Guid>
    {
        public Guid OrderId { get; private set; }
        public IEnumerable<SeatAssignment> Assignments { get; private set; }

        public SeatAssignmentsCreated(Guid sourceId, Guid orderId, IEnumerable<SeatAssignment> assignments) : base(sourceId)
        {
            OrderId = orderId;
            Assignments = assignments;
        }
    }
}
