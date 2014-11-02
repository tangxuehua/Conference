using System;
using ENode.Commanding;

namespace Registration.Commands
{
    public class ConfirmOrder : AggregateCommand<Guid>
    {
        public ConfirmOrder(Guid orderId) : base(orderId) { }
    }
}
