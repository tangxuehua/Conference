using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatsReservationCancelled : DomainEvent<Guid>
    {
        public Guid ReservationId { get; set; }
        public IEnumerable<SeatAvailableQuantity> SeatAvailableQuantities { get; set; }

        public SeatsReservationCancelled(Conference conference, Guid reservationId, IEnumerable<SeatAvailableQuantity> seatAvailableQuantities)
            : base(conference)
        {
            SeatAvailableQuantities = seatAvailableQuantities;
        }
    }
}