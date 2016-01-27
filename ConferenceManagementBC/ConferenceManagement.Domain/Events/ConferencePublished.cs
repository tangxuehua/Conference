using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class ConferencePublished : DomainEvent<Guid>
    {
        public ConferencePublished() { }
    }
}
