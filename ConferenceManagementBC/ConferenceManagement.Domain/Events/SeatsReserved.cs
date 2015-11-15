using System;
using System.Collections.Generic;
using ENode.Eventing;
using ENode.Infrastructure;

namespace ConferenceManagement
{
    [Serializable]
    [Code(1306)]
    public class SeatsReserved : DomainEvent<Guid>
    {
        public Guid ReservationId { get; private set; }
        public IEnumerable<ReservationItem> ReservationItems { get; private set; }
        public IEnumerable<SeatAvailableQuantity> SeatAvailableQuantities { get; private set; }

        public SeatsReserved() { }
        public SeatsReserved(Conference conference, Guid reservationId, IEnumerable<ReservationItem> reservationItems, IEnumerable<SeatAvailableQuantity> seatAvailableQuantities)
            : base(conference)
        {
            ReservationId = reservationId;
            ReservationItems = reservationItems;
            SeatAvailableQuantities = seatAvailableQuantities;
        }
    }
}