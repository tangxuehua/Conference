using Conference.Common;
using ECommon.Components;
using ENode.EQueue;
using ENode.Infrastructure;

namespace ConferenceManagement.ProcessorHost.TopicProviders
{
    [Component]
    public class ApplicationMessageTopicProvider : AbstractTopicProvider<IApplicationMessage>
    {
        public override string GetTopic(IApplicationMessage applicationMessage)
        {
            return Topics.ConferenceApplicationMessageTopic;
        }
    }
}
