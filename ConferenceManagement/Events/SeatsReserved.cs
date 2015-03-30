using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatsReserved : DomainEvent<Guid>
    {
        public Guid ReservationId { get; private set; }
        public IEnumerable<ReservationItem> ReservationItems { get; private set; }
        public IEnumerable<SeatAvailableQuantity> SeatAvailableQuantities { get; private set; }

        public SeatsReserved(Conference conference, Guid reservationId, IEnumerable<ReservationItem> reservationItems, IEnumerable<SeatAvailableQuantity> seatAvailableQuantities)
            : base(conference)
        {
            ReservationId = reservationId;
            ReservationItems = reservationItems;
            SeatAvailableQuantities = seatAvailableQuantities;
        }
    }
}