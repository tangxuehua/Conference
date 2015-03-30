using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatTypeUpdated : SeatTypeEvent
    {
        public SeatTypeUpdated(Conference conference, Guid seatTypeId, SeatTypeInfo seatTypeInfo)
            : base(conference, seatTypeId, seatTypeInfo) { }
    }
}
