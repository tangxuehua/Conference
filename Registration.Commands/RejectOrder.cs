using System;
using ENode.Commanding;

namespace Registration.Commands
{
    [Serializable]
    public class RejectOrder : AggregateCommand<Guid>
    {
        public RejectOrder(Guid orderId) : base(orderId) { }
    }
}
