namespace Payments.ReadModel.Implementation
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Conference.Common;
    using ECommon.Components;
    using ECommon.Dapper;

    [Component]
    public class PaymentDao : IPaymentDao
    {
        public Payment GetPayment(Guid paymentId)
        {
            using (var connection = GetConnection())
            {
                return connection.QueryList<Payment>(new { Id = paymentId }, ConfigSettings.PaymentTable).SingleOrDefault();
            }
        }
        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ConnectionString);
        }
    }
}
