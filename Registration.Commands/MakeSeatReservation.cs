using System;
using System.Collections.Generic;

namespace Registration.Commands
{
    public class MakeSeatReservation : SeatsAvailabilityCommand
    {
        public Guid ReservationId { get; set; }
        public IEnumerable<SeatInfo> Seats { get; set; }

        public MakeSeatReservation(Guid conferenceId) : base(conferenceId)
        {
            this.Seats = new List<SeatInfo>();
        }
    }
}
