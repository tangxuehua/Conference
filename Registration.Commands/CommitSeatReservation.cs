using System;

namespace Registration.Commands
{
    public class CommitSeatReservation : SeatsAvailabilityCommand
    {
        public Guid ReservationId { get; set; }

        public CommitSeatReservation(Guid conferenceId) : base(conferenceId) { }
    }
}
