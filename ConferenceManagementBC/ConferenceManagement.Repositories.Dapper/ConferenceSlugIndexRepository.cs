using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Conference.Common;
using ConferenceManagement.Domain.Models;
using ConferenceManagement.Domain.Repositories;
using ECommon.Components;
using ECommon.Dapper;

namespace ConferenceManagement.Repositories.Dapper
{
    [Component]
    public class ConferenceSlugIndexRepository : IConferenceSlugIndexRepository
    {
        public async Task Add(ConferenceSlugIndex index)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                await connection.InsertAsync(new
                {
                    IndexId = index.IndexId,
                    ConferenceId = index.ConferenceId,
                    Slug = index.Slug
                }, ConfigSettings.ConferenceSlugIndexTable);
            }
        }
        public async Task<ConferenceSlugIndex> FindSlugIndex(string slug)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var data = await connection.QueryListAsync(new { Slug = slug }, ConfigSettings.ConferenceSlugIndexTable);
                var conferenceSlugIndex = data.SingleOrDefault();
                if (conferenceSlugIndex != null)
                {
                    var indexId = conferenceSlugIndex.IndexId as string;
                    var conferenceId = (Guid)conferenceSlugIndex.ConferenceId;
                    return new ConferenceSlugIndex(indexId, conferenceId, slug);
                }
                return null;
            }
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ConferenceConnectionString);
        }
    }
}
