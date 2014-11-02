using Conference.Contracts;
using ECommon.Components;
using ENode.Eventing;
using Registration.Commands;

namespace Registration.EventHandlers
{
    [Component]
    public class SeatsAvailabilityEventHandler : IEventHandler<ConferenceCreated>
    {
        public void Handle(IEventContext context, ConferenceCreated evnt)
        {
            context.AddCommand(new CreateSeatsAvailability(evnt.AggregateRootId));
        }
    }
}
