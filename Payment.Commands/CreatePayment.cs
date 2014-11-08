using System;
using ENode.Commanding;

namespace Payments.Commands
{
    [Serializable]
    public class CreatePayment : AggregateCommand<Guid>, ICreatingAggregateCommand
    {
        public Guid OrderId { get; set; }
        public Guid ConferenceId { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
