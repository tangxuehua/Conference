using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace Registration.Commands.Orders
{
    [Serializable]
    [Code(3103)]
    public class CloseOrder : Command<Guid>
    {
        public CloseOrder() { }
        public CloseOrder(Guid orderId) : base(orderId) { }
    }
}
