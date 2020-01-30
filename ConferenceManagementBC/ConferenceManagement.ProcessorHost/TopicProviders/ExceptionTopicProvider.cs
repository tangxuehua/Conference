using Conference.Common;
using ECommon.Components;
using ENode.Domain;
using ENode.EQueue;

namespace ConferenceManagement.ProcessorHost.TopicProviders
{
    [Component]
    public class ExceptionTopicProvider : AbstractTopicProvider<IDomainException>
    {
        public override string GetTopic(IDomainException exception)
        {
            return Topics.ConferenceExceptionTopic;
        }
    }
}
