using System;
using ENode.Commanding;

namespace Payments.Commands
{
    [Serializable]
    public class CancelPayment : Command<Guid>
    {
    }
}
