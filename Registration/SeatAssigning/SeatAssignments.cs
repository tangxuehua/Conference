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
        private IEnumerable<SeatAssignment> _seats;

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

        public void AssignSeat(int position, RegistrantInfo attendee)
        {
            var current = this._seats.SingleOrDefault(x => x.Position == position);
            if (current == null)
            {
                throw new ArgumentOutOfRangeException("position");
            }
            if (attendee.Email != current.Attendee.Email || attendee.FirstName != current.Attendee.FirstName || attendee.LastName != current.Attendee.LastName)
            {
                ApplyEvent(new SeatAssigned(this._id, current.Position, current.SeatType, attendee));
            }
        }
        public void UnassignSeat(int position)
        {
            var current = this._seats.SingleOrDefault(x => x.Position == position);
            if (current == null)
            {
                throw new ArgumentOutOfRangeException("position");
            }
            ApplyEvent(new SeatUnassigned(this._id, position));
        }

        private void Handle(SeatAssignmentsCreated e)
        {
            this._id = e.AggregateRootId;
            this._orderId = e.OrderId;
            this._seats = e.Seats;
        }
        private void Handle(SeatAssigned e)
        {
            this._seats.Single(x => x.Position == e.Position).Attendee = e.Attendee;
        }
        private void Handle(SeatUnassigned e)
        {
            this._seats.Single(x => x.Position == e.Position).Attendee = null;
        }
    }
    [Serializable]
    public class SeatAssignment
    {
        public int Position { get; private set; }
        public Guid SeatType { get; private set; }
        public RegistrantInfo Attendee { get; set; }

        public SeatAssignment(int position, Guid seatType)
        {
            Position = position;
            SeatType = seatType;
        }
    }
}
