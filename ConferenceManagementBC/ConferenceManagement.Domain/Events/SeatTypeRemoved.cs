using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace ConferenceManagement
{
    [Serializable]
    [Code(1309)]
    public class SeatTypeRemoved : DomainEvent<Guid>
    {
        public Guid SeatTypeId { get; private set; }

        public SeatTypeRemoved() { }
        public SeatTypeRemoved(Conference conference, Guid seatTypeId)
            : base(conference)
        {
            SeatTypeId = seatTypeId;
        }
    }
}
