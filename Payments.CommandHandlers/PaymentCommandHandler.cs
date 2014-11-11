using System.Linq;
using ECommon.Components;
using ENode.Commanding;
using Payments;
using Payments.Commands;

namespace Payments.CommandHandlers
{
    [Component]
    public class PaymentCommandHandler :
        ICommandHandler<CreatePayment>,
        ICommandHandler<CompletePayment>,
        ICommandHandler<CancelPayment>
    {
        public void Handle(ICommandContext context, CreatePayment command)
        {
            context.Add(new Payment(
                command.AggregateRootId,
                command.OrderId,
                command.ConferenceId,
                command.Description,
                command.TotalAmount,
                command.Lines.Select(x => new PaymentItem(x.Description, x.Amount)).ToList()));
        }
        public void Handle(ICommandContext context, CompletePayment command)
        {
            context.Get<Payments.Payment>(command.AggregateRootId).Complete();
        }
        public void Handle(ICommandContext context, CancelPayment command)
        {
            context.Get<Payments.Payment>(command.AggregateRootId).Cancel();
        }
    }
}
