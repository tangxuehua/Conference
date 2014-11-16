using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatTypeAdded : SeatTypeEvent
    {
        public int Quantity { get; private set; }

        public SeatTypeAdded(Guid conferenceId, Guid seatTypeId, SeatTypeInfo seatTypeInfo, int quantity)
            : base(conferenceId, seatTypeId, seatTypeInfo)
        {
            Quantity = quantity;
        }
    }
}
