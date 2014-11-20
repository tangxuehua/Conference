using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatTypeEvent : DomainEvent<Guid>
    {
        public Guid SeatTypeId { get; private set; }
        public SeatTypeInfo SeatTypeInfo { get; private set; }

        public SeatTypeEvent(Guid conferenceId, Guid seatTypeId, SeatTypeInfo seatTypeInfo) : base(conferenceId)
        {
            SeatTypeId = seatTypeId;
            SeatTypeInfo = seatTypeInfo;
        }
    }
}
