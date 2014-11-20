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
        private Guid _orderId;
        private IEnumerable<SeatAssignment> _assignments;

        public SeatAssignments(Guid id, Guid orderId, IEnumerable<SeatQuantity> seats) : base(id)
        {
            var i = 0;
            var all = new List<SeatAssignment>();
            foreach (var seatQuantity in seats)
            {
                for (int j = 0; j < seatQuantity.Quantity; j++)
                {
                    all.Add(new SeatAssignment(i++, seatQuantity.SeatType));
                }
            }
            ApplyEvent(new SeatAssignmentsCreated(id, orderId, all));
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
                ApplyEvent(new SeatAssigned(_id, current.Position, current.SeatType, attendee));
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
