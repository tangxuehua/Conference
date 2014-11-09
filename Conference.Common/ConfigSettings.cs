using System.Configuration;

namespace Conference.Common
{
    public class ConfigSettings
    {
        public static string ConnectionString { get; set; }

        public static void Initialize()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }
    }
}
