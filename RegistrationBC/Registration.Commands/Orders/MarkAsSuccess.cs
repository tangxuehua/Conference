using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace Registration.Commands.Orders
{
    [Serializable]
    [Code(3104)]
    public class MarkAsSuccess : Command<Guid>
    {
        public MarkAsSuccess() { }
        public MarkAsSuccess(Guid orderId) : base(orderId) { }
    }
}
