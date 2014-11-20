using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderReservationConfirmed : OrderEvent
    {
        public OrderReservationConfirmed(Guid orderId, Guid conferenceId) : base(orderId, conferenceId) { }
    }
}
