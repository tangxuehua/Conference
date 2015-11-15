using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace Payments.Commands
{
    [Serializable]
    [Code(2102)]
    public class CompletePayment : Command<Guid>
    {
    }
}
