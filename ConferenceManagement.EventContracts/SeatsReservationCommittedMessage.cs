using System;
using ENode.Infrastructure;

namespace ConferenceManagement.Messages
{
    [Serializable]
    public class SeatsReservationCommittedMessage : ApplicationMessage
    {
        public Guid ConferenceId { get; set; }
        public Guid ReservationId { get; set; }
    }
}