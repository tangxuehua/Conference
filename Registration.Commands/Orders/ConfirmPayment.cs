using System;
using ENode.Commanding;

namespace Registration.Commands.Orders
{
    [Serializable]
    public class ConfirmPayment : AggregateCommand<Guid>
    {
        public bool IsPaymentSuccess { get; set; }

        public ConfirmPayment(Guid orderId, bool isPaymentSuccess) : base(orderId)
        {
            IsPaymentSuccess = isPaymentSuccess;
        }
    }
}
