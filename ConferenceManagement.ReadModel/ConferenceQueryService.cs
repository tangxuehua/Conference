using System.Configuration;
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
        public ConferenceInfo FindConference(string slug)
        {
            using (var connection = GetConnection())
            {
                return connection.QueryList<ConferenceInfo>(new { Slug = slug }, ConfigSettings.ConferenceTable).SingleOrDefault();
            }
        }
        public ConferenceInfo FindConference(string email, string accessCode)
        {
            using (var connection = GetConnection())
            {
                return connection.QueryList<ConferenceInfo>(new { OwnerEmail = email, AccessCode = accessCode }, ConfigSettings.ConferenceTable).SingleOrDefault();
            }
        }

        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ConferenceConnectionString);
        }
    }
}
