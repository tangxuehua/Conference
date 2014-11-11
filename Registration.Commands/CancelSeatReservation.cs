using System;

namespace Registration.Commands
{
    [Serializable]
    public class CancelSeatReservation : SeatsAvailabilityCommand
    {
        public Guid ReservationId { get; set; }

        public CancelSeatReservation(Guid conferenceId) : base(conferenceId) { }
    }
}
