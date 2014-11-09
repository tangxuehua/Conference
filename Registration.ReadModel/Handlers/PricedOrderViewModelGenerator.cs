using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Conference.Common;
using ECommon.Components;
using ECommon.Dapper;
using ENode.Eventing;
using Registration.Orders;
using Registration.ReadModel;
using Registration.ReadModel.Implementation;
using Registration.SeatAssigning;

namespace Registration.Handlers
{
    [Component]
    public class PricedOrderViewModelGenerator :
        IEventHandler<OrderPlaced>,
        IEventHandler<OrderTotalsCalculated>,
        IEventHandler<OrderConfirmed>,
        IEventHandler<SeatAssignmentsCreated>
    {
        public void Handle(IEventContext eventContext, OrderPlaced evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Insert(new PricedOrder { OrderId = evnt.AggregateRootId, OrderVersion = evnt.Version }, "PricedOrdersV3");
            }
        }
        public void Handle(IEventContext eventContext, OrderTotalsCalculated evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    var seatTypeIds = evnt.Lines.OfType<SeatOrderLine>().Select(x => x.SeatType).Distinct().ToArray();

                    connection.Delete(new { OrderId = evnt.AggregateRootId }, "PricedOrderLinesV3");

                    var seatTypeDescriptions = GetSeatTypeDescriptions(seatTypeIds, connection);

                    for (int i = 0; i < evnt.Lines.Length; i++)
                    {
                        var orderLine = evnt.Lines[i];
                        var line = new PricedOrderLine
                        {
                            LineTotal = orderLine.LineTotal,
                            Position = i,
                        };

                        var seatOrderLine = orderLine as SeatOrderLine;
                        if (seatOrderLine != null)
                        {
                            line.Description = seatTypeDescriptions.Where(x => x.SeatTypeId == seatOrderLine.SeatType).Select(x => x.Name).FirstOrDefault();
                            line.UnitPrice = seatOrderLine.UnitPrice;
                            line.Quantity = seatOrderLine.Quantity;
                        }

                        connection.Insert(line, "PricedOrderLinesV3");
                    }

                    connection.Update(new { Total = evnt.Total, IsFreeOfCharge = evnt.IsFreeOfCharge, OrderVersion = evnt.Version }, new { OrderId = evnt.AggregateRootId }, "PricedOrdersV3");

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void Handle(IEventContext eventContext, OrderConfirmed evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Update(new { ReservationExpirationDate = default(DateTime?), OrderVersion = evnt.Version }, new { OrderId = evnt.AggregateRootId }, "PricedOrdersV3");
            }
        }
        public void Handle(IEventContext eventContext, SeatAssignmentsCreated evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Update(new { AssignmentsId = evnt.AggregateRootId }, new { OrderId = evnt.OrderId }, "PricedOrdersV3");
            }
        }

        private List<PricedOrderLineSeatTypeDescription> GetSeatTypeDescriptions(IEnumerable<Guid> seatTypeIds, IDbConnection connection)
        {
            var result = new List<PricedOrderLineSeatTypeDescription>();
            foreach (var seatTypeId in seatTypeIds)
            {
                var item = connection.QueryList<PricedOrderLineSeatTypeDescription>(new { SeatTypeId = seatTypeId }, "PricedOrderLineSeatTypeDescriptionsV3").SingleOrDefault();
                if (item != null)
                {
                    result.Add(item);
                }
            }
            return result;
        }
        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ConnectionString);
        }
    }
}
