using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatsReservationCommitted : DomainEvent<Guid>
    {
        public Guid ReservationId { get; set; }

        public SeatsReservationCommitted(Guid conferenceId, Guid reservationId) : base(conferenceId) { }
    }
}