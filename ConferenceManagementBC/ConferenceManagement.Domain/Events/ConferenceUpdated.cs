using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace ConferenceManagement
{
    [Serializable]
    [Code(1303)]
    public class ConferenceUpdated : DomainEvent<Guid>
    {
        public ConferenceEditableInfo Info { get; private set; }

        public ConferenceUpdated() { }
        public ConferenceUpdated(Conference conference, ConferenceEditableInfo info)
            : base(conference)
        {
            Info = info;
        }
    }
}
