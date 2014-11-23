using System;
using System.Collections.Generic;
using System.Linq;
using ENode.Domain;

namespace ConferenceManagement
{
    [Serializable]
    public class Conference : AggregateRoot<Guid>
    {
        private ConferenceInfo _info;
        private IList<SeatType> _seatTypes;
        private IDictionary<Guid, IEnumerable<ReservationItem>> _reservations;
        private bool _isPublished;

        public Conference(Guid id, ConferenceInfo info) : base(id)
        {
            ApplyEvent(new ConferenceCreated(id, info));
        }

        public void Update(ConferenceInfo info)
        {
            ApplyEvent(new ConferenceUpdated(_id, info));
        }
        public void Publish()
        {
            ApplyEvent(new ConferencePublished(_id));
        }
        public void Unpublish()
        {
            ApplyEvent(new ConferenceUnpublished(_id));
        }
        public void AddSeat(SeatTypeInfo seatTypeInfo, int quantity)
        {
            if (_isPublished)
            {
                throw new Exception("Published conference cannot add seat type.");
            }
            ApplyEvent(new SeatTypeAdded(_id, Guid.NewGuid(), seatTypeInfo, quantity));
        }
        public void UpdateSeat(Guid seatTypeId, SeatTypeInfo seatTypeInfo, int quantity)
        {
            var seatType = _seatTypes.SingleOrDefault(x => x.Id == seatTypeId);
            if (seatType == null)
            {
                throw new Exception("Seat type not exist.");
            }
            ApplyEvent(new SeatTypeUpdated(_id, seatTypeId, seatTypeInfo));

            if (seatType.Quantity != quantity)
            {
                var totalReservationQuantity = GetTotalReservationQuantity(seatType.Id);
                if (quantity < totalReservationQuantity)
                {
                    throw new Exception(string.Format("Quantity cannot be small than total reservation quantity:{0}", totalReservationQuantity));
                }
                ApplyEvent(new SeatTypeQuantityChanged(_id, seatTypeId, quantity, quantity - totalReservationQuantity));
            }
        }
        public void RemoveSeat(Guid seatTypeId)
        {
            if (_isPublished)
            {
                throw new Exception("Can't delete seat type from a conference that has been published");
            }
            if (HasReservation(seatTypeId))
            {
                throw new Exception("The seat type has reservation, cannot be remove.");
            }
            ApplyEvent(new SeatTypeRemoved(_id, seatTypeId));
        }
        public void MakeReservation(Guid reservationId, IEnumerable<ReservationItem> reservationItems)
        {
            if (!_isPublished)
            {
                throw new Exception("Can't make reservation to the conference which is not published.");
            }
            if (_reservations.ContainsKey(reservationId))
            {
                throw new Exception(string.Format("Duplicated reservation, reservationId:{0}", reservationId));
            }
            if (reservationItems == null || reservationItems.Count() == 0)
            {
                throw new Exception(string.Format("Reservation items can't be null or empty, reservationId:{0}", reservationId));
            }

            foreach (var reservationItem in reservationItems)
            {
                if (reservationItem.Quantity <= 0)
                {
                    throw new Exception(string.Format("Quantity must be bigger than than zero, reservationId:{0}, seatTypeId:{1}", reservationId, reservationItem.SeatTypeId));
                }
                var seatType = _seatTypes.SingleOrDefault(x => x.Id == reservationItem.SeatTypeId);
                if (seatType == null)
                {
                    throw new ArgumentOutOfRangeException(string.Format("Seat type '{0}' not exist.", reservationItem.SeatTypeId));
                }
                var availableQuantity = seatType.Quantity - GetTotalReservationQuantity(seatType.Id);
                if (availableQuantity < reservationItem.Quantity)
                {
                    throw new SeatInsufficientException(_id, reservationId);
                }
            }
            ApplyEvent(new SeatsReserved(_id, reservationId, reservationItems));
        }
        public void CommitReservation(Guid reservationId)
        {
            if (_reservations.ContainsKey(reservationId))
            {
                ApplyEvent(new SeatsReservationCommitted(_id, reservationId));
            }
        }
        public void CancelReservation(Guid reservationId)
        {
            if (_reservations.ContainsKey(reservationId))
            {
                ApplyEvent(new SeatsReservationCancelled(_id, reservationId));
            }
        }

        private bool HasReservation(Guid seatTypeId)
        {
            return _reservations.Any(x => x.Value.Any(y => y.SeatTypeId == seatTypeId));
        }
        private int GetTotalReservationQuantity(Guid seatTypeId)
        {
            var totalReservationQuantity = 0;
            foreach (var reservation in _reservations)
            {
                var reservationItem = reservation.Value.SingleOrDefault(x => x.SeatTypeId == seatTypeId);
                if (reservationItem != null)
                {
                    totalReservationQuantity += reservationItem.Quantity;
                }
            }
            return totalReservationQuantity;
        }

        #region Event Handle Methods

        private void Handle(ConferenceCreated evnt)
        {
            _id = evnt.AggregateRootId;
            _info = evnt.Info;
            _seatTypes = new List<SeatType>();
            _reservations = new Dictionary<Guid, IEnumerable<ReservationItem>>();
            _isPublished = false;
        }
        private void Handle(ConferenceUpdated evnt)
        {
            _info = evnt.Info;
        }
        private void Handle(ConferencePublished evnt)
        {
            _isPublished = true;
        }
        private void Handle(ConferenceUnpublished evnt)
        {
            _isPublished = false;
        }
        private void Handle(SeatTypeAdded evnt)
        {
            _seatTypes.Add(new SeatType(evnt.SeatTypeId, evnt.SeatTypeInfo) { Quantity = evnt.Quantity });
        }
        private void Handle(SeatTypeUpdated evnt)
        {
            _seatTypes.Single(x => x.Id == evnt.SeatTypeId).Info = evnt.SeatTypeInfo;
        }
        private void Handle(SeatTypeQuantityChanged evnt)
        {
            _seatTypes.Single(x => x.Id == evnt.SeatTypeId).Quantity = evnt.Quantity;
        }
        private void Handle(SeatTypeRemoved evnt)
        {
            _seatTypes.Remove(_seatTypes.Single(x => x.Id == evnt.SeatTypeId));
        }
        private void Handle(SeatsReserved evnt)
        {
            _reservations.Add(evnt.ReservationId, evnt.ReservationItems.ToList());
        }
        private void Handle(SeatsReservationCommitted evnt)
        {
            foreach (var reservationItem in _reservations[evnt.ReservationId])
            {
                _seatTypes.Single(x => x.Id == reservationItem.SeatTypeId).Quantity -= reservationItem.Quantity;
            }
            _reservations.Remove(evnt.ReservationId);
        }
        private void Handle(SeatsReservationCancelled evnt)
        {
            _reservations.Remove(evnt.ReservationId);
        }

        #endregion
    }
}
