using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
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
