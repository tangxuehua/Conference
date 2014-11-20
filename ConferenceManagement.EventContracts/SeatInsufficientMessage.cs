using System;
using ENode.Messaging;

namespace ConferenceManagement.Messages
{
    [Serializable]
    public class SeatInsufficientMessage : Message
    {
        public Guid ConferenceId { get; set; }
        public Guid ReservationId { get; set; }
    }
}