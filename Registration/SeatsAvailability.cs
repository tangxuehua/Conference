using System;
using System.Collections.Generic;
using System.Linq;
using ENode.Domain;
using Registration.Events;

namespace Registration
{
    [Serializable]
    public class SeatsAvailability : AggregateRoot<Guid>
    {
        private Dictionary<Guid, int> _remainingSeats;
        private Dictionary<Guid, List<SeatQuantity>> _pendingReservations;

        public SeatsAvailability(Guid id) : base(id)
        {
            ApplyEvent(new SeatsAvailabilityCreated(id));
        }

        public void AddSeats(Guid seatType, int quantity)
        {
            ApplyEvent(new AvailableSeatsChanged(Id) { Seats = new[] { new SeatQuantity(seatType, quantity) } });
        }
        public void RemoveSeats(Guid seatType, int quantity)
        {
            ApplyEvent(new AvailableSeatsChanged(Id) { Seats = new[] { new SeatQuantity(seatType, -quantity) } });
        }
        public void MakeReservation(Guid reservationId, IEnumerable<SeatQuantity> wantedSeats)
        {
            var wantedList = wantedSeats.ToList();
            if (wantedList.Any(x => !this._remainingSeats.ContainsKey(x.SeatType)))
            {
                throw new ArgumentOutOfRangeException("wantedSeats");
            }

            var difference = new Dictionary<Guid, SeatDifference>();

            foreach (var w in wantedList)
            {
                var item = GetOrAdd(difference, w.SeatType);
                item.Wanted = w.Quantity;
                item.Remaining = this._remainingSeats[w.SeatType];
            }

            List<SeatQuantity> existing;
            if (this._pendingReservations.TryGetValue(reservationId, out existing))
            {
                foreach (var e in existing)
                {
                    GetOrAdd(difference, e.SeatType).Existing = e.Quantity;
                }
            }

            var reservation = new SeatsReserved(reservationId)
            {
                ReservationDetails = difference.Select(x => new SeatQuantity(x.Key, x.Value.Actual)).Where(x => x.Quantity != 0).ToList(),
                AvailableSeatsChanged = difference.Select(x => new SeatQuantity(x.Key, -x.Value.DeltaSinceLast)).Where(x => x.Quantity != 0).ToList()
            };

            ApplyEvent(reservation);
        }
        public void CancelReservation(Guid reservationId)
        {
            List<SeatQuantity> reservation;
            if (this._pendingReservations.TryGetValue(reservationId, out reservation))
            {
                ApplyEvent(new SeatsReservationCancelled(reservationId)
                {
                    AvailableSeatsChanged = reservation.Select(x => new SeatQuantity(x.SeatType, x.Quantity)).ToList()
                });
            }
        }
        public void CommitReservation(Guid reservationId)
        {
            if (this._pendingReservations.ContainsKey(reservationId))
            {
                ApplyEvent(new SeatsReservationCommitted(reservationId));
            }
        }

        private class SeatDifference
        {
            public int Wanted { get; set; }
            public int Existing { get; set; }
            public int Remaining { get; set; }
            public int Actual
            {
                get { return Math.Min(this.Wanted, Math.Max(this.Remaining, 0) + this.Existing); }
            }
            public int DeltaSinceLast
            {
                get { return this.Actual - this.Existing; }
            }
        }

        private static TValue GetOrAdd<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
            {
                value = new TValue();
                dictionary[key] = value;
            }

            return value;
        }

        private void Handle(SeatsAvailabilityCreated e)
        {
            _id = e.AggregateRootId;
            _remainingSeats = new Dictionary<Guid, int>();
            _pendingReservations = new Dictionary<Guid, List<SeatQuantity>>();
        }
        private void Handle(AvailableSeatsChanged e)
        {
            foreach (var seat in e.Seats)
            {
                int newValue = seat.Quantity;
                int remaining;
                if (this._remainingSeats.TryGetValue(seat.SeatType, out remaining))
                {
                    newValue += remaining;
                }

                this._remainingSeats[seat.SeatType] = newValue;
            }
        }
        private void Handle(SeatsReserved e)
        {
            var details = e.ReservationDetails.ToList();
            if (details.Count > 0)
            {
                this._pendingReservations[e.AggregateRootId] = details;
            }
            else
            {
                this._pendingReservations.Remove(e.AggregateRootId);
            }

            foreach (var seat in e.AvailableSeatsChanged)
            {
                this._remainingSeats[seat.SeatType] = this._remainingSeats[seat.SeatType] + seat.Quantity;
            }
        }
        private void Handle(SeatsReservationCommitted e)
        {
            this._pendingReservations.Remove(e.AggregateRootId);
        }
        private void Handle(SeatsReservationCancelled e)
        {
            this._pendingReservations.Remove(e.AggregateRootId);

            foreach (var seat in e.AvailableSeatsChanged)
            {
                this._remainingSeats[seat.SeatType] = this._remainingSeats[seat.SeatType] + seat.Quantity;
            }
        }
    }
}
