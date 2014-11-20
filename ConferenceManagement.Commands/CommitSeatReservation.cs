using System;
using ENode.Commanding;

namespace ConferenceManagement.Commands
{
    [Serializable]
    public class CommitSeatReservation : AggregateCommand<Guid>
    {
        public Guid ReservationId { get; set; }

        public CommitSeatReservation(Guid conferenceId, Guid reservationId) : base(conferenceId)
        {
            ReservationId = reservationId;
        }
    }
}
