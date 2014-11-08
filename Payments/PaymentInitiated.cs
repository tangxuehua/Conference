using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Payments
{
    [Serializable]
    public class PaymentInitiated : DomainEvent<Guid>
    {
        public Guid OrderId { get; private set; }
        public Guid ConferenceId { get; private set; }
        public string Description { get; private set; }
        public decimal TotalAmount { get; private set; }

        public PaymentInitiated(Guid paymentId, Guid orderId, Guid conferenceId, string description, decimal totalAmount) : base(paymentId)
        {
            OrderId = orderId;
            ConferenceId = conferenceId;
            Description = description;
            TotalAmount = totalAmount;
        }
    }
}
