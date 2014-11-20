using System;

namespace ConferenceManagement.Messages
{
    [Serializable]
    public class SeatReservationItem
    {
        public Guid SeatTypeId { get; set; }
        public int Quantity { get; set; }
    }
}
