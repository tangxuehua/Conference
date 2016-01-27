using System;
using System.Collections.Generic;
using ENode.Infrastructure;

namespace ConferenceManagement.Messages
{
    [Serializable]
    public class SeatsReservedMessage : ApplicationMessage
    {
        public Guid ConferenceId { get; set; }
        public Guid ReservationId { get; set; }
        public IEnumerable<SeatReservationItem> ReservationItems { get; set; }
    }
}