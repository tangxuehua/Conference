using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace ConferenceManagement
{
    [Serializable]
    [Code(1308)]
    public class SeatTypeQuantityChanged : DomainEvent<Guid>
    {
        public Guid SeatTypeId { get; private set; }
        public int Quantity { get; private set; }
        public int AvailableQuantity { get; private set; }

        public SeatTypeQuantityChanged() { }
        public SeatTypeQuantityChanged(Conference conference, Guid seatTypeId, int quantity, int availableQuantity)
            : base(conference)
        {
            SeatTypeId = seatTypeId;
            Quantity = quantity;
            AvailableQuantity = availableQuantity;
        }
    }
}
