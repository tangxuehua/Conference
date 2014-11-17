using System;

namespace ConferenceManagement.Commands
{
    [Serializable]
    public class SeatReservationItemInfo
    {
        public Guid SeatType { get; set; }
        public int Quantity { get; set; }
    }
}
