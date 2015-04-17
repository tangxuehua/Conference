using Conference.Common;
using ECommon.Components;
using ENode.EQueue;
using ENode.Eventing;
using ENode.Infrastructure;

namespace ConferenceManagement.CommandProcessor.TopicProviders
{
    [Component]
    public class ExceptionTopicProvider : AbstractTopicProvider<IPublishableException>
    {
        public override string GetTopic(IPublishableException exception)
        {
            return Topics.ConferenceExceptionTopic;
        }
    }
}
