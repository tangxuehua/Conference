using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace ConferenceManagement
{
    [Serializable]
    [Code(1302)]
    public class ConferenceUnpublished : DomainEvent<Guid>
    {
        public ConferenceUnpublished() { }
        public ConferenceUnpublished(Conference conference) : base(conference) { }
    }
}
