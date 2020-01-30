using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Conference.Common;
using ECommon.Components;
using ECommon.Dapper;
using ENode.Messaging;
using Registration.Orders;

namespace Registration.Handlers
{
    [Component]
    public class OrderViewModelGenerator :
        IMessageHandler<OrderPlaced>,
        IMessageHandler<OrderRegistrantAssigned>,
        IMessageHandler<OrderReservationConfirmed>,
        IMessageHandler<OrderPaymentConfirmed>,
        IMessageHandler<OrderExpired>,
        IMessageHandler<OrderClosed>,
        IMessageHandler<OrderSuccessed>
    {
        public Task HandleAsync(OrderPlaced evnt)
        {
            return TryTransactionAsync((connection, transaction) =>
            {
                var tasks = new List<Task>();

                //插入订单主记录
                tasks.Add(connection.InsertAsync(new
                {
                    OrderId = evnt.AggregateRootId,
                    ConferenceId = evnt.ConferenceId,
                    Status = (int)OrderStatus.Placed,
                    AccessCode = evnt.AccessCode,
                    ReservationExpirationDate = evnt.ReservationExpirationDate,
                    TotalAmount = evnt.OrderTotal.Total,
                    Version = evnt.Version
                }, ConfigSettings.OrderTable, transaction));

                //插入订单明细
                foreach (var line in evnt.OrderTotal.Lines)
                {
                    tasks.Add(connection.InsertAsync(new
                    {
                        OrderId = evnt.AggregateRootId,
                        SeatTypeId = line.SeatQuantity.Seat.SeatTypeId,
                        SeatTypeName = line.SeatQuantity.Seat.SeatTypeName,
                        Quantity = line.SeatQuantity.Quantity,
                        UnitPrice = line.SeatQuantity.Seat.UnitPrice,
                        LineTotal = line.LineTotal
                    }, ConfigSettings.OrderLineTable, transaction));
                }

                return tasks;
            });
        }
        public Task HandleAsync(OrderRegistrantAssigned evnt)
        {
            return TryUpdateRecordAsync(connection =>
            {
                return connection.UpdateAsync(new
                {
                    RegistrantFirstName = evnt.Registrant.FirstName,
                    RegistrantLastName = evnt.Registrant.LastName,
                    RegistrantEmail = evnt.Registrant.Email,
                    Version = evnt.Version
                }, new
                {
                    OrderId = evnt.AggregateRootId,
                    Version = evnt.Version - 1
                }, ConfigSettings.OrderTable);
            });
        }
        public Task HandleAsync(OrderReservationConfirmed evnt)
        {
            return TryUpdateRecordAsync(connection =>
            {
                return connection.UpdateAsync(new
                {
                    Status = (int)evnt.OrderStatus,
                    Version = evnt.Version
                }, new
                {
                    OrderId = evnt.AggregateRootId,
                    Version = evnt.Version - 1
                }, ConfigSettings.OrderTable);
            });
        }
        public Task HandleAsync(OrderPaymentConfirmed evnt)
        {
            return TryUpdateRecordAsync(connection =>
            {
                return connection.UpdateAsync(new
                {
                    Status = (int)evnt.OrderStatus,
                    Version = evnt.Version
                }, new
                {
                    OrderId = evnt.AggregateRootId,
                    Version = evnt.Version - 1
                }, ConfigSettings.OrderTable);
            });
        }
        public Task HandleAsync(OrderExpired evnt)
        {
            return TryUpdateRecordAsync(connection =>
            {
                return connection.UpdateAsync(new
                {
                    Status = (int)OrderStatus.Expired,
                    Version = evnt.Version
                }, new
                {
                    OrderId = evnt.AggregateRootId,
                    Version = evnt.Version - 1
                }, ConfigSettings.OrderTable);
            });
        }
        public Task HandleAsync(OrderClosed evnt)
        {
            return TryUpdateRecordAsync(connection =>
            {
                return connection.UpdateAsync(new
                {
                    Status = (int)OrderStatus.Closed,
                    Version = evnt.Version
                }, new
                {
                    OrderId = evnt.AggregateRootId,
                    Version = evnt.Version - 1
                }, ConfigSettings.OrderTable);
            });
        }
        public Task HandleAsync(OrderSuccessed evnt)
        {
            return TryUpdateRecordAsync(connection =>
            {
                return connection.UpdateAsync(new
                {
                    Status = (int)OrderStatus.Success,
                    Version = evnt.Version
                }, new
                {
                    OrderId = evnt.AggregateRootId,
                    Version = evnt.Version - 1
                }, ConfigSettings.OrderTable);
            });
        }

        private async Task TryUpdateRecordAsync(Func<IDbConnection, Task<int>> action)
        {
            using (var connection = GetConnection())
            {
                await action(connection);
            }
        }
        private async Task TryTransactionAsync(Func<IDbConnection, IDbTransaction, IEnumerable<Task>> actions)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var transaction = await Task.Run<SqlTransaction>(() => connection.BeginTransaction()).ConfigureAwait(false);
                try
                {
                    await Task.WhenAll(actions(connection, transaction)).ConfigureAwait(false);
                    await Task.Run(() => transaction.Commit()).ConfigureAwait(false);
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ConferenceConnectionString);
        }
    }
}
