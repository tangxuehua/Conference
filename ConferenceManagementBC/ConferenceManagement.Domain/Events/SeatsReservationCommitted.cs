using System;
using System.Collections.Generic;
using ENode.Eventing;
using ENode.Infrastructure;

namespace ConferenceManagement
{
    [Serializable]
    [Code(1305)]
    public class SeatsReservationCommitted : DomainEvent<Guid>
    {
        public Guid ReservationId { get; set; }
        public IEnumerable<SeatQuantity> SeatQuantities { get; set; }

        public SeatsReservationCommitted() { }
        public SeatsReservationCommitted(Conference conference, Guid reservationId, IEnumerable<SeatQuantity> seatQuantities) : base(conference)
        {
            ReservationId = reservationId;
            SeatQuantities = seatQuantities;
        }
    }
}