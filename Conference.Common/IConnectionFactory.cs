using System.Data;

namespace Conference.Common
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
