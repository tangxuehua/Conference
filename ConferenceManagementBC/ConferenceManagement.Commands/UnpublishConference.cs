using System;
using ENode.Commanding;

namespace ConferenceManagement.Commands
{
    [Serializable]
    public class UnpublishConference : Command<Guid>
    {
    }
}
