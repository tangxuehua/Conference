using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Conference.Common;
using ECommon.Components;
using ECommon.Dapper;

namespace ConferenceManagement.ReadModel
{
    [Component]
    public class ConferenceQueryService
    {
        public ConferenceDTO FindConference(string slug)
        {
            using (var connection = GetConnection())
            {
                return connection.QueryList<ConferenceDTO>(new { Slug = slug }, ConfigSettings.ConferenceTable).SingleOrDefault();
            }
        }
        public ConferenceDTO FindConference(string email, string accessCode)
        {
            using (var connection = GetConnection())
            {
                return connection.QueryList<ConferenceDTO>(new { OwnerEmail = email, AccessCode = accessCode }, ConfigSettings.ConferenceTable).SingleOrDefault();
            }
        }
        public IEnumerable<SeatTypeDTO> FindSeatTypes(Guid conferenceId)
        {
            using (var connection = GetConnection())
            {
                return connection.QueryList<SeatTypeDTO>(new { ConferenceId = conferenceId }, ConfigSettings.SeatTypeTable).ToList();
            }
        }
        public SeatTypeDTO FindSeatType(Guid seatTypeId)
        {
            using (var connection = GetConnection())
            {
                return connection.QueryList<SeatTypeDTO>(new { Id = seatTypeId }, ConfigSettings.SeatTypeTable).SingleOrDefault();
            }
        }

        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ConferenceConnectionString);
        }
    }
}
