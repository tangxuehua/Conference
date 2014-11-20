using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderPaymentConfirmed : OrderEvent
    {
        public bool IsPaymentSuccess { get; private set; }

        public OrderPaymentConfirmed(Guid orderId, Guid conferenceId, bool isPaymentSuccess) : base(orderId, conferenceId)
        {
            IsPaymentSuccess = isPaymentSuccess;
        }
    }
}
