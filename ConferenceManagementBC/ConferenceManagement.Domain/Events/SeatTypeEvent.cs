using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public abstract class SeatTypeEvent : DomainEvent<Guid>
    {
        public Guid SeatTypeId { get; private set; }
        public SeatTypeInfo SeatTypeInfo { get; private set; }

        public SeatTypeEvent() { }
        public SeatTypeEvent(Conference conference, Guid seatTypeId, SeatTypeInfo seatTypeInfo)
            : base(conference)
        {
            SeatTypeId = seatTypeId;
            SeatTypeInfo = seatTypeInfo;
        }
    }
}
