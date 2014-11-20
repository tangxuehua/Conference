using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderReservationConfirmed : OrderEvent
    {
        public bool IsReservationSuccess { get; private set; }

        public OrderReservationConfirmed(Guid orderId, Guid conferenceId, bool isReservationSuccess) : base(orderId, conferenceId)
        {
            IsReservationSuccess = isReservationSuccess;
        }
    }
}
