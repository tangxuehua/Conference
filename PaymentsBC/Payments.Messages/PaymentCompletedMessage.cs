using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace Payments.Messages
{
    [Serializable]
    [Code(2600)]
    public class PaymentCompletedMessage : PaymentMessage
    {
    }
}
