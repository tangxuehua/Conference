using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace ConferenceManagement
{
    [Serializable]
    public abstract class SeatTypeEvent : DomainEvent<Guid>
    {
        public Guid SeatTypeId { get; private set; }
        public SeatTypeInfo SeatTypeInfo { get; private set; }

        public SeatTypeEvent() { }
        public SeatTypeEvent(Guid seatTypeId, SeatTypeInfo seatTypeInfo)
        {
            SeatTypeId = seatTypeId;
            SeatTypeInfo = seatTypeInfo;
        }
    }
}
