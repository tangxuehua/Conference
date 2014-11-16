using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatsReservationAdded : DomainEvent<Guid>
    {
        public Guid ReservationId { get; private set; }
        public IEnumerable<ReservationItem> ReservationItems { get; private set; }

        public SeatsReservationAdded(Guid conferenceId, Guid reservationId, IEnumerable<ReservationItem> reservationItems)
            : base(conferenceId)
        {
            ReservationId = reservationId;
            ReservationItems = reservationItems;
        }
    }
}