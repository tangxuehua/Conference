using System;
using System.Collections.Generic;
using ENode.Commanding;

namespace Payments.Contracts.Commands
{
    public class CreatePayment : AggregateCommand<Guid>, ICreatingAggregateCommand
    {
        public class PaymentItem
        {
            public string Description { get; set; }
            public decimal Amount { get; set; }
        }

        public CreatePayment() : base(Guid.NewGuid())
        {
            this.PaymentItems = new List<CreatePayment.PaymentItem>();
        }

        public Guid PaymentSourceId { get; set; }
        public Guid ConferenceId { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public IList<CreatePayment.PaymentItem> PaymentItems { get; private set; }
    }
}
