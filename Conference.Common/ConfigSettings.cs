using System;
using System.Configuration;
using System.Linq;

namespace Conference.Common
{
    public class ConfigSettings
    {
        public static TimeSpan ReservationAutoExpiration = TimeSpan.FromMinutes(15);
        public static string ConferenceEQueueConnectionString { get; set; }
        public static string ConferenceENodeConnectionString { get; set; }
        public static string ConferenceConnectionString { get; set; }
        public static string ConferenceTable { get; set; }
        public static string ConferenceSlugIndexTable { get; set; }
        public static string SeatTypeTable { get; set; }
        public static string ReservationItemsTable { get; set; }
        public static string OrderTable { get; set; }
        public static string OrderLineTable { get; set; }
        public static string OrderSeatAssignmentsTable { get; set; }
        public static string PaymentTable { get; set; }
        public static string PaymentItemTable { get; set; }

        public static void Initialize()
        {
            if (ConfigurationManager.ConnectionStrings["equeue"] != null)
            {
                ConferenceEQueueConnectionString = ConfigurationManager.ConnectionStrings["equeue"].ConnectionString;
            }
            if (ConfigurationManager.ConnectionStrings["enode"] != null)
            {
                ConferenceENodeConnectionString = ConfigurationManager.ConnectionStrings["enode"].ConnectionString;
            }
            if (ConfigurationManager.ConnectionStrings["conference"] != null)
            {
                ConferenceConnectionString = ConfigurationManager.ConnectionStrings["conference"].ConnectionString;
            }
            ConferenceTable = "Conferences";
            ConferenceSlugIndexTable = "ConferenceSlugs";
            SeatTypeTable = "ConferenceSeatTypes";
            ReservationItemsTable = "ReservationItems";
            OrderTable = "Orders";
            OrderLineTable = "OrderLines";
            OrderSeatAssignmentsTable = "OrderSeatAssignments";
            PaymentTable = "ThirdPartyProcessorPayments";
            PaymentItemTable = "ThidPartyProcessorPaymentItems";
        }
    }
}
