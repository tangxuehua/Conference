using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public abstract class ConferenceEvent : DomainEvent<Guid>
    {
        public ConferenceInfo Info { get; private set; }

        public ConferenceEvent(Guid id, ConferenceInfo info) : base(id)
        {
            Info = info;
        }
    }
}
