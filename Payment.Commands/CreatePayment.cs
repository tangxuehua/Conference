using System;
using System.Collections.Generic;
using ENode.Commanding;
using Payment.Commands;

namespace Payments.Commands
{
    [Serializable]
    public class CreatePayment : AggregateCommand<Guid>, ICreatingAggregateCommand
    {
        public Guid OrderId { get; set; }
        public Guid ConferenceId { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<PaymentLine> Lines { get; set; }
    }
}
