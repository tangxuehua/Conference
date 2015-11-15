using System;
using ENode.Infrastructure;

namespace ConferenceManagement.Messages
{
    [Serializable]
    [Code(1601)]
    public class SeatsReservationCommittedMessage : ApplicationMessage
    {
        public Guid ConferenceId { get; set; }
        public Guid ReservationId { get; set; }
    }
}