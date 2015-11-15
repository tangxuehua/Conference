using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace ConferenceManagement.Commands
{
    [Serializable]
    [Code(1107)]
    public class UnpublishConference : Command<Guid>
    {
    }
}
