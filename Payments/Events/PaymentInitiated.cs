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
        public IEnumerable<PaymentItem> Items { get; private set; }

        public PaymentInitiated(Payment payment, Guid orderId, Guid conferenceId, string description, decimal totalAmount, IEnumerable<PaymentItem> items)
            : base(payment)
        {
            OrderId = orderId;
            ConferenceId = conferenceId;
            Description = description;
            TotalAmount = totalAmount;
            Items = items;
        }
    }
}
