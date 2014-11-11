using System;

namespace Registration.Commands
{
    [Serializable]
    public class AddSeats : SeatsAvailabilityCommand
    {
        public Guid SeatType { get; set; }
        public int Quantity { get; set; }

        public AddSeats(Guid conferenceId) : base(conferenceId) { }
    }
}
