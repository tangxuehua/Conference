using System;
using ENode.Messaging;

namespace ConferenceManagement.Messages
{
    [Serializable]
    public class SeatsReservationCancelledMessage : VersionedMessage<Guid>
    {
        public Guid ReservationId { get; set; }
    }
}