using Conference.Common;
using ECommon.Components;
using ENode.EQueue;
using ENode.Eventing;

namespace ConferenceManagement.ProcessorHost.TopicProviders
{
    [Component]
    public class DomainEventTopicProvider : AbstractTopicProvider<IDomainEvent>
    {
        public override string GetTopic(IDomainEvent evnt)
        {
            return Topics.ConferenceDomainEventTopic;
        }
    }
}
