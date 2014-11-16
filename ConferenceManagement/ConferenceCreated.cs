using System;

namespace ConferenceManagement
{
    [Serializable]
    public class ConferenceCreated : ConferenceEvent
    {
        public ConferenceCreated(Guid id, ConferenceInfo info) : base(id, info)
        {
        }
    }
}
