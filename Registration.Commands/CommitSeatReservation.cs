using System;

namespace Registration.Commands
{
    [Serializable]
    public class CommitSeatReservation : SeatsAvailabilityCommand
    {
        public Guid ReservationId { get; set; }

        public CommitSeatReservation(Guid conferenceId) : base(conferenceId) { }
    }
}
