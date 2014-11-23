using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatTypeQuantityChanged : DomainEvent<Guid>
    {
        public Guid SeatTypeId { get; private set; }
        public int Quantity { get; private set; }
        public int AvailableQuantity { get; private set; }

        public SeatTypeQuantityChanged(Guid conferenceId, Guid seatTypeId, int quantity, int availableQuantity)  : base(conferenceId)
        {
            SeatTypeId = seatTypeId;
            Quantity = quantity;
            AvailableQuantity = availableQuantity;
        }
    }
}
