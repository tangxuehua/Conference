namespace Payments.ReadModel
{
    using System;

    public class PaymentItem
    {
        public Guid Id { get; private set; }
        public Guid PaymentId { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
    }
}