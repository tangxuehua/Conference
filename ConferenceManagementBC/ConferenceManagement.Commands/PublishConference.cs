using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace ConferenceManagement.Commands
{
    [Serializable]
    [Code(1105)]
    public class PublishConference : Command<Guid>
    {
    }
}
