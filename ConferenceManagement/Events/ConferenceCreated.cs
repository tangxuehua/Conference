using System;

namespace ConferenceManagement
{
    [Serializable]
    public class ConferenceCreated : ConferenceEvent
    {
        public ConferenceCreated() { }
        public ConferenceCreated(Conference conference, ConferenceInfo info)
            : base(conference, info)
        {
        }
    }
}
