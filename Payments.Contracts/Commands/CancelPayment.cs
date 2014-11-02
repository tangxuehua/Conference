using System;
using ENode.Commanding;

namespace Payments.Contracts.Commands
{
    public class CancelPayment : AggregateCommand<Guid>
    {
        public CancelPayment(Guid paymentId) : base(paymentId) { }
    }
}
