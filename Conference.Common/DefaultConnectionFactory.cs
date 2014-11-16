using System.Data;
using System.Data.SqlClient;
using ECommon.Components;

namespace Conference.Common
{
    [Component]
    public class DefaultConnectionFactory : IConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConfigSettings.ConnectionString);
        }
    }
}
