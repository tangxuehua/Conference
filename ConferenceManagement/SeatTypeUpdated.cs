using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatTypeUpdated : SeatTypeEvent
    {
        public SeatTypeUpdated(Guid conferenceId, Guid seatTypeId, SeatTypeInfo seatTypeInfo)
            : base(conferenceId, seatTypeId, seatTypeInfo) { }
    }
}
