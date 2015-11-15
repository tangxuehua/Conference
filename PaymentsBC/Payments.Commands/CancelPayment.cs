using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace Payments.Commands
{
    [Serializable]
    [Code(2101)]
    public class CancelPayment : Command<Guid>
    {
    }
}
