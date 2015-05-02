using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.SeatAssigning
{
    [Serializable]
    public class OrderSeatAssignmentsCreated : DomainEvent<Guid>
    {
        public Guid OrderId { get; private set; }
        public IEnumerable<SeatAssignment> Assignments { get; private set; }

        public OrderSeatAssignmentsCreated() { }
        public OrderSeatAssignmentsCreated(OrderSeatAssignments source, Guid orderId, IEnumerable<SeatAssignment> assignments) : base(source)
        {
            OrderId = orderId;
            Assignments = assignments;
        }
    }
}
