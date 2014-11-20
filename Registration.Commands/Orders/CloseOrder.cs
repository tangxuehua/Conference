using System;
using ENode.Commanding;

namespace Registration.Commands.Orders
{
    [Serializable]
    public class CloseOrder : AggregateCommand<Guid>
    {
        public CloseOrder(Guid orderId) : base(orderId) { }
    }
}
