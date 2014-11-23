namespace Payments.ReadModel
{
    using System;

    public class Payment
    {
        public Guid Id { get; private set; }
        public Guid SourceId { get; private set; }
        public int State { get; private set; }
        public string Description { get; private set; }
        public decimal TotalAmount { get; private set; }
    }
}
