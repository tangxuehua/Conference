using System;
using System.Collections.Generic;
using ENode.Messaging;

namespace ConferenceManagement.Messages
{
    [Serializable]
    public class SeatsReservedMessage : VersionedMessage<Guid>
    {
        public Guid ReservationId { get; set; }
        public IEnumerable<SeatReservationItem> ReservationItems { get; set; }
    }
}