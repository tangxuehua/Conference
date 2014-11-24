using System;

namespace Registration.SeatAssigning
{
    [Serializable]
    public class SeatAssignment
    {
        public int Position { get; private set; }
        public SeatType Seat { get; private set; }
        public Attendee Attendee { get; set; }

        public SeatAssignment(int position, SeatType seat)
        {
            Position = position;
            Seat = seat;
        }
    }
}
