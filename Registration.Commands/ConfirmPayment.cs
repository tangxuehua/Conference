using System;
using ENode.Commanding;

namespace Registration.Commands
{
    [Serializable]
    public class ConfirmPayment : AggregateCommand<Guid>
    {
        public ConfirmPayment(Guid orderId) : base(orderId) { }
    }
}
