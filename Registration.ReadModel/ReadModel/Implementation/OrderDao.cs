using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Conference.Common;
using ECommon.Components;
using ECommon.Dapper;

namespace Registration.ReadModel.Implementation
{
    [Component]
    public class OrderDao : IOrderDao
    {
        public Guid? LocateOrder(string email, string accessCode)
        {
            using (var connection = GetConnection())
            {
                var draftOrder = connection.QueryList<DraftOrder>(new { RegistrantEmail = email, AccessCode = accessCode }, "OrdersViewV3").FirstOrDefault();
                if (draftOrder != null)
                {
                    return draftOrder.OrderId;
                }
                return null;
            }
        }

        public DraftOrder FindDraftOrder(Guid orderId)
        {
            using (var connection = GetConnection())
            {
                var draftOrder = connection.QueryList<DraftOrder>(new { OrderId = orderId }, "OrdersViewV3").FirstOrDefault();
                if (draftOrder != null)
                {
                    draftOrder.Lines = connection.QueryList<DraftOrderItem>(new { OrderId = orderId }, "OrderItemsViewV3").ToList();
                    return draftOrder;
                }
                return null;
            }
        }

        public PricedOrder FindPricedOrder(Guid orderId)
        {
            using (var connection = GetConnection())
            {
                var pricedOrder = connection.QueryList<PricedOrder>(new { OrderId = orderId }, "PricedOrdersV3").FirstOrDefault();
                if (pricedOrder != null)
                {
                    pricedOrder.Lines = connection.QueryList<PricedOrderLine>(new { OrderId = orderId }, "PricedOrderLinesV3").ToList();
                    return pricedOrder;
                }
                return null;
            }
        }

        public OrderSeats FindOrderSeats(Guid assignmentsId)
        {
            //TODO
            //return FindBlob<OrderSeats>(SeatAssignmentsViewModelGenerator.GetSeatAssignmentsBlobId(assignmentsId));
            return null;
        }

        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ConnectionString);
        }
    }
}