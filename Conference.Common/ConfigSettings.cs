using System;
using System.Configuration;

namespace Conference.Common
{
    public class ConfigSettings
    {
        public static TimeSpan ReservationAutoExpiration = TimeSpan.FromMinutes(15);
        public static string ConferenceTable { get; set; }
        public static string SeatTypeTable { get; set; }
        public static string ReservationItemsTable { get; set; }
        public static string OrderTable { get; set; }
        public static string OrderLineTable { get; set; }
        public static string OrderSeatAssignmentsTable { get; set; }
        public static string PaymentTable { get; set; }
        public static string PaymentItemTable { get; set; }

        public static void Initialize()
        {
            ConferenceTable = "Conferences";
            SeatTypeTable = "ConferenceSeatTypesView";
            ReservationItemsTable = "ReservationItems";
            OrderTable = "Orders";
            OrderLineTable = "OrderLines";
            OrderSeatAssignmentsTable = "OrderSeatAssignments";
            PaymentTable = "ThirdPartyProcessorPayments";
            PaymentItemTable = "ThidPartyProcessorPaymentItems";
        }
    }
}
