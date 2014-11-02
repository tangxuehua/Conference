using System;
using System.Collections.Generic;

namespace Registration.Commands
{
    public class MakeSeatReservation : SeatsAvailabilityCommand
    {
        public Guid ReservationId { get; set; }
        public List<SeatQuantity> Seats { get; set; }

        public MakeSeatReservation(Guid conferenceId) : base(conferenceId)
        {
            this.Seats = new List<SeatQuantity>();
        }
    }
}
