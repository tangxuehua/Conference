using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace Payments.Messages
{
    [Serializable]
    [Code(2601)]
    public class PaymentRejectedMessage : PaymentMessage
    {
    }
}
