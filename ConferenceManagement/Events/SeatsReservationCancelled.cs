using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatsReservationCancelled : DomainEvent<Guid>
    {
        public Guid ReservationId { get; set; }

        public SeatsReservationCancelled(Guid conferenceId, Guid reservationId) : base(conferenceId) { }
    }
}