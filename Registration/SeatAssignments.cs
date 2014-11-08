using System;
using System.Collections.Generic;
using System.Linq;
using ENode.Domain;
using Registration.Events;

namespace Registration
{
    [Serializable]
    public class SeatAssignments : AggregateRoot<Guid>
    {
        private class SeatAssignment
        {
            public SeatAssignment()
            {
                this.Attendee = new PersonalInfo();
            }

            public int Position { get; set; }
            public Guid SeatType { get; set; }
            public PersonalInfo Attendee { get; set; }
        }
        private Guid _orderId;
        private Dictionary<int, SeatAssignment> _seats;

        public SeatAssignments(Guid id, Guid orderId, IEnumerable<SeatQuantity> seats) : base(id)
        {
            // Add as many assignments as seats there are.
            var i = 0;
            var all = new List<SeatAssignmentsCreated.SeatAssignmentInfo>();
            foreach (var seatQuantity in seats)
            {
                for (int j = 0; j < seatQuantity.Quantity; j++)
                {
                    all.Add(new SeatAssignmentsCreated.SeatAssignmentInfo { Position = i++, SeatType = seatQuantity.SeatType });
                }
            }

            ApplyEvent(new SeatAssignmentsCreated(id) { OrderId = orderId, Seats = all });
        }

        public void AssignSeat(int position, PersonalInfo attendee)
        {
            if (string.IsNullOrEmpty(attendee.Email))
                throw new ArgumentNullException("attendee.Email");

            SeatAssignment current;
            if (!this._seats.TryGetValue(position, out current))
                throw new ArgumentOutOfRangeException("position");

            if (!attendee.Email.Equals(current.Attendee.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                if (current.Attendee.Email != null)
                {
                    ApplyEvent(new SeatUnassigned(this.Id) { Position = position });
                }

                ApplyEvent(new SeatAssigned(this.Id)
                {
                    Position = position,
                    SeatType = current.SeatType,
                    Attendee = attendee,
                });
            }
            else if (!string.Equals(attendee.FirstName, current.Attendee.FirstName, StringComparison.InvariantCultureIgnoreCase)
                || !string.Equals(attendee.LastName, current.Attendee.LastName, StringComparison.InvariantCultureIgnoreCase))
            {
                ApplyEvent(new SeatAssignmentUpdated(this.Id)
                {
                    Position = position,
                    Attendee = attendee,
                });
            }
        }
        public void Unassign(int position)
        {
            SeatAssignment current;
            if (!this._seats.TryGetValue(position, out current))
                throw new ArgumentOutOfRangeException("position");

            if (current.Attendee.Email != null)
            {
                ApplyEvent(new SeatUnassigned(this.Id) { Position = position });
            }
        }

        private void Handle(SeatAssignmentsCreated e)
        {
            this._id = e.AggregateRootId;
            this._orderId = e.OrderId;
            this._seats = e.Seats.ToDictionary(x => x.Position, x => new SeatAssignment { Position = x.Position, SeatType = x.SeatType });
        }
        private void Handle(SeatAssigned e)
        {
            this._seats[e.Position] = new SeatAssignment
            {
                Position = e.Position,
                SeatType = e.SeatType,
                Attendee = e.Attendee
            };
        }
        private void Handle(SeatUnassigned e)
        {
            this._seats[e.Position] = new SeatAssignment { Position = e.Position, SeatType = this._seats[e.Position].SeatType };
        }
        private void Handle(SeatAssignmentUpdated e)
        {
            this._seats[e.Position] = new SeatAssignment
            {
                Position = e.Position,
                // Seat type is also never received again from the client.
                SeatType = this._seats[e.Position].SeatType,
                // The email property is not received for updates, as those 
                // are for the same attendee essentially.
                Attendee = new PersonalInfo { Email = this._seats[e.Position].Attendee.Email }
            };
        }
    }
}
