using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class ConferenceUnpublished : DomainEvent<Guid>
    {
        public ConferenceUnpublished() { }
    }
}
