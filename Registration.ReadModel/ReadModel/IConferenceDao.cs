using System;
using System.Collections.Generic;

namespace Registration.ReadModel
{
    public interface IConferenceDao
    {
        ConferenceDetails GetConferenceDetails(string slug);
        ConferenceAlias GetConferenceAlias(string slug);

        IList<ConferenceAlias> GetPublishedConferences();
        IList<SeatType> GetPublishedSeatTypes(Guid conferenceId);
        IList<SeatTypeName> GetSeatTypeNames(IEnumerable<Guid> seatTypes);
    }
}