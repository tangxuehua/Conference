using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.SeatAssigning
{
    public class SeatAssignmentsCreated : DomainEvent<Guid>
    {
        public Guid OrderId { get; private set; }
        public IEnumerable<SeatAssignment> Seats { get; private set; }

        public SeatAssignmentsCreated(Guid sourceId, Guid orderId, IEnumerable<SeatAssignment> seats) : base(sourceId)
        {
            OrderId = orderId;
            Seats = seats;
        }
    }
}
