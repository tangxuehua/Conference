using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatTypeUpdated : SeatTypeEvent
    {
        public SeatTypeUpdated() { }
        public SeatTypeUpdated(Guid seatTypeId, SeatTypeInfo seatTypeInfo)
            : base(seatTypeId, seatTypeInfo) { }
    }
}
