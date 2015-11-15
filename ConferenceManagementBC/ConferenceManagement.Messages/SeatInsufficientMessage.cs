using System;
using ENode.Infrastructure;

namespace ConferenceManagement.Messages
{
    [Serializable]
    [Code(1603)]
    public class SeatInsufficientMessage : ApplicationMessage
    {
        public Guid ConferenceId { get; set; }
        public Guid ReservationId { get; set; }
    }
}