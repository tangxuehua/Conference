using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace Registration.Commands.Orders
{
    [Serializable]
    [Code(3102)]
    public class ConfirmPayment : Command<Guid>
    {
        public bool IsPaymentSuccess { get; set; }

        public ConfirmPayment() { }
        public ConfirmPayment(Guid orderId, bool isPaymentSuccess) : base(orderId)
        {
            IsPaymentSuccess = isPaymentSuccess;
        }
    }
}
