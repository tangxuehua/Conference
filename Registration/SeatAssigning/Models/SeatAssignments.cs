using System;
using System.Collections.Generic;
using System.Linq;
using ENode.Domain;
using Registration.Orders;

namespace Registration.SeatAssigning
{
    [Serializable]
    public class SeatAssignments : AggregateRoot<Guid>
    {
        private IEnumerable<SeatAssignment> _assignments;

        public SeatAssignments(Guid orderId, IEnumerable<OrderLine> orderLines) : base(orderId)
        {
            var position = 0;
            var assignments = new List<SeatAssignment>();
            foreach (var orderLine in orderLines)
            {
                for (int j = 0; j < orderLine.SeatQuantity.Quantity; j++)
                {
                    assignments.Add(new SeatAssignment(position++, orderLine.SeatQuantity.Seat));
                }
            }
            ApplyEvent(new SeatAssignmentsCreated(orderId, assignments));
        }
        public void AssignSeat(int position, Attendee attendee)
        {
            var current = _assignments.SingleOrDefault(x => x.Position == position);
            if (current == null)
            {
                throw new ArgumentOutOfRangeException("position");
            }
            if (attendee.Email != current.Attendee.Email || attendee.FirstName != current.Attendee.FirstName || attendee.LastName != current.Attendee.LastName)
            {
                ApplyEvent(new SeatAssigned(_id, current.Position, current.Seat, attendee));
            }
        }
        public void UnassignSeat(int position)
        {
            var current = _assignments.SingleOrDefault(x => x.Position == position);
            if (current == null)
            {
                throw new ArgumentOutOfRangeException("position");
            }
            ApplyEvent(new SeatUnassigned(_id, position));
        }

        private void Handle(SeatAssignmentsCreated evnt)
        {
            _id = evnt.AggregateRootId;
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
