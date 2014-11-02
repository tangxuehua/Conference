using System;
using ENode.Commanding;

namespace Payments.Contracts.Commands
{
    public class CompletePayment : AggregateCommand<Guid>
    {
        public CompletePayment(Guid paymentId) : base(paymentId) { }
    }
}
