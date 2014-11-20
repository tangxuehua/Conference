using System;
using ENode.Messaging;

namespace Payments.Messages
{
    [Serializable]
    public abstract class PaymentMessage : VersionedMessage<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid ConferenceId { get; set; }
    }
}
