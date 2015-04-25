using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatTypeAdded : SeatTypeEvent
    {
        public int Quantity { get; private set; }

        public SeatTypeAdded() { }
        public SeatTypeAdded(Conference conference, Guid seatTypeId, SeatTypeInfo seatTypeInfo, int quantity)
            : base(conference, seatTypeId, seatTypeInfo)
        {
            Quantity = quantity;
        }
    }
}
