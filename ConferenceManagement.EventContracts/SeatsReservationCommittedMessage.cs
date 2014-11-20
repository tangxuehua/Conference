using System;
using ENode.Messaging;

namespace ConferenceManagement.Messages
{
    [Serializable]
    public class SeatsReservationCommittedMessage : VersionedMessage<Guid>
    {
        public Guid ReservationId { get; set; }
    }
}