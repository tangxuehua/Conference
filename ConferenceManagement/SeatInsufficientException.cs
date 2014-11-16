using System;
using System.Collections.Generic;
using ENode.Exceptions;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatInsufficientException : PublishableException, IPublishableException
    {
        public Guid ConferenceId { get; private set; }
        public Guid ReservationId { get; private set; }

        public SeatInsufficientException(Guid conferenceId, Guid reservationId) : base()
        {
            ConferenceId = conferenceId;
            ReservationId = reservationId;
        }

        public void RestoreFrom(IDictionary<string, string> serializableInfo)
        {
            serializableInfo.Add("ConferenceId", ConferenceId.ToString());
            serializableInfo.Add("ReservationId", ReservationId.ToString());
        }
        public void SerializeTo(IDictionary<string, string> serializableInfo)
        {
            ConferenceId = Guid.Parse(serializableInfo["ConferenceId"]);
            ReservationId = Guid.Parse(serializableInfo["ReservationId"]);
        }
    }
}
