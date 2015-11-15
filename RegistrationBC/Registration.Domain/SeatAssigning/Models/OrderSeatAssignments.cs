using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Utilities;
using ENode.Domain;
using ENode.Infrastructure;
using Registration.Orders;

namespace Registration.SeatAssigning
{
    [Serializable]
    [Code(3201)]
    public class OrderSeatAssignments : AggregateRoot<Guid>
    {
        private Guid _orderId;
        private IEnumerable<SeatAssignment> _assignments;

        public OrderSeatAssignments(Guid orderId, IEnumerable<OrderLine> orderLines) : base(Guid.NewGuid())
        {
            Ensure.NotEmptyGuid(orderId, "orderId");
            Ensure.NotNull(orderLines, "orderLines");
            if (!orderLines.Any()) throw new ArgumentException("The seats of order cannot be empty.");

            var position = 0;
            var assignments = new List<SeatAssignment>();
            foreach (var orderLine in orderLines)
            {
                for (int i = 0; i < orderLine.SeatQuantity.Quantity; i++)
                {
                    assignments.Add(new SeatAssignment(position++, orderLine.SeatQuantity.Seat));
                }
            }
            ApplyEvent(new OrderSeatAssignmentsCreated(this, orderId, assignments));
        }
        public void AssignSeat(int position, Attendee attendee)
        {
            var current = _assignments.SingleOrDefault(x => x.Position == position);
            if (current == null)
            {
                throw new ArgumentOutOfRangeException("position");
            }
            if (current.Attendee == null || attendee != current.Attendee)
            {
                ApplyEvent(new SeatAssigned(this, current.Position, current.Seat, attendee));
            }
        }
        public void UnassignSeat(int position)
        {
            var current = _assignments.SingleOrDefault(x => x.Position == position);
            if (current == null)
            {
                throw new ArgumentOutOfRangeException("position");
            }
            ApplyEvent(new SeatUnassigned(this, position));
        }

        private void Handle(OrderSeatAssignmentsCreated evnt)
        {
            _id = evnt.AggregateRootId;
            _orderId = evnt.OrderId;
            _assignments = evnt.Assignments;
        }
        private void Handle(SeatAssigned evnt)
        {
            _assignments.Single(x => x.Position == evnt.Position).Attendee = evnt.Attendee;
        }
        private void Handle(SeatUnassigned evnt)
        {
            _assignments.Single(x => x.Position == evnt.Position).Attendee = null;
        }
    }
}
