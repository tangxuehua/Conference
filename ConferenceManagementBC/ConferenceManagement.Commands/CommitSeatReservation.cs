using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace ConferenceManagement.Commands
{
    [Serializable]
    [Code(1102)]
    public class CommitSeatReservation : Command<Guid>
    {
        public Guid ReservationId { get; set; }

        public CommitSeatReservation() { }
        public CommitSeatReservation(Guid conferenceId, Guid reservationId) : base(conferenceId)
        {
            ReservationId = reservationId;
        }
    }
}
