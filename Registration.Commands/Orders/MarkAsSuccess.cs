using System;
using ENode.Commanding;

namespace Registration.Commands.Orders
{
    [Serializable]
    public class MarkAsSuccess : AggregateCommand<Guid>
    {
        public MarkAsSuccess(Guid orderId) : base(orderId) { }
    }
}
