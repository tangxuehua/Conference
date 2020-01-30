using System;
using System.Collections.Generic;
using ENode.Messaging;

namespace ConferenceManagement.Messages
{
    public class SeatsReservedMessage : ApplicationMessage
    {
        public Guid ConferenceId { get; set; }
        public Guid ReservationId { get; set; }
        public IEnumerable<SeatReservationItem> ReservationItems { get; set; }
    }
}