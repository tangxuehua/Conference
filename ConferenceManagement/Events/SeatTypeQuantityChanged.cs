using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatTypeQuantityChanged : DomainEvent<Guid>
    {
        public Guid SeatTypeId { get; private set; }
        public int Quantity { get; private set; }

        public SeatTypeQuantityChanged(Guid conferenceId, Guid seatTypeId, int quantity)  : base(conferenceId)
        {
            SeatTypeId = seatTypeId;
            Quantity = quantity;
        }
    }
}
