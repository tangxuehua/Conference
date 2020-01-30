using System;
using ENode.Messaging;

namespace ConferenceManagement.Messages
{
    public class SeatInsufficientMessage : ApplicationMessage
    {
        public Guid ConferenceId { get; set; }
        public Guid ReservationId { get; set; }
    }
}