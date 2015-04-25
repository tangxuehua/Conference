using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatsReservationCommitted : DomainEvent<Guid>
    {
        public Guid ReservationId { get; set; }
        public IEnumerable<SeatQuantity> SeatQuantities { get; set; }

        public SeatsReservationCommitted() { }
        public SeatsReservationCommitted(Conference conference, Guid reservationId, IEnumerable<SeatQuantity> seatQuantities) : base(conference)
        {
            SeatQuantities = seatQuantities;
        }
    }
}