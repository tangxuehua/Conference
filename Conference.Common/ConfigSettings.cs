using System.Configuration;

namespace Conference.Common
{
    public class ConfigSettings
    {
        public static string ConnectionString { get; set; }
        public static string ConferenceTable { get; set; }
        public static string SeatTypeTable { get; set; }
        public static string ReservationItemsTable { get; set; }

        public static void Initialize()
        {
            ConferenceTable = "Conferences";
            SeatTypeTable = "ConferenceSeatTypesView";
            ReservationItemsTable = "ReservationItems";
            ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }
    }
}
