using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace ConferenceManagement
{
    [Serializable]
    [Code(1301)]
    public class ConferencePublished : DomainEvent<Guid>
    {
        public ConferencePublished() { }
        public ConferencePublished(Conference conference) : base(conference) { }
    }
}
