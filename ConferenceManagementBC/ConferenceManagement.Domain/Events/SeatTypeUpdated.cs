using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace ConferenceManagement
{
    [Serializable]
    [Code(1310)]
    public class SeatTypeUpdated : SeatTypeEvent
    {
        public SeatTypeUpdated() { }
        public SeatTypeUpdated(Conference conference, Guid seatTypeId, SeatTypeInfo seatTypeInfo)
            : base(conference, seatTypeId, seatTypeInfo) { }
    }
}
