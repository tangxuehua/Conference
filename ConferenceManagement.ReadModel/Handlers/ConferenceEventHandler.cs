using System.Linq;
using Conference.Common;
using ECommon.Components;
using ECommon.Dapper;
using ENode.Eventing;

namespace ConferenceManagement.ReadModel.Handlers
{
    [Component]
    public class ConferenceEventHandler :
        IEventHandler<ConferenceCreated>,
        IEventHandler<ConferenceUpdated>,
        IEventHandler<ConferencePublished>,
        IEventHandler<ConferenceUnpublished>,
        IEventHandler<SeatTypeAdded>,
        IEventHandler<SeatTypeUpdated>,
        IEventHandler<SeatTypeQuantityChanged>,
        IEventHandler<SeatTypeRemoved>,
        IEventHandler<SeatsReserved>,
        IEventHandler<SeatsReservationCommitted>,
        IEventHandler<SeatsReservationCancelled>
    {
        private IConnectionFactory _connectionFactory;

        public ConferenceEventHandler(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Handle(IEventContext context, ConferenceCreated evnt)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var info = evnt.Info;
                connection.Insert(new
                {
                    Id = evnt.AggregateRootId,
                    AccessCode = info.AccessCode,
                    OwnerName = info.Owner.Name,
                    OwnerEmail = info.Owner.Email,
                    Slug = info.Slug,
                    Name = info.Name,
                    Description = info.Description,
                    Location = info.Location,
                    Tagline = info.Tagline,
                    TwitterSearch = info.TwitterSearch,
                    StartDate = info.StartDate,
                    EndDate = info.EndDate,
                    IsPublished = 0
                }, ConfigSettings.ConferenceTable);
            }
        }
        public void Handle(IEventContext context, ConferenceUpdated evnt)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var info = evnt.Info;
                connection.Update(new
                {
                    AccessCode = info.AccessCode,
                    OwnerName = info.Owner.Name,
                    OwnerEmail = info.Owner.Email,
                    Slug = info.Slug,
                    Name = info.Name,
                    Description = info.Description,
                    Location = info.Location,
                    Tagline = info.Tagline,
                    TwitterSearch = info.TwitterSearch,
                    StartDate = info.StartDate,
                    EndDate = info.EndDate,
                    IsPublished = 0
                }, new { Id = evnt.AggregateRootId }, ConfigSettings.ConferenceTable);
            }
        }
        public void Handle(IEventContext context, ConferencePublished evnt)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Update(new { IsPublished = 1 }, new { Id = evnt.AggregateRootId }, ConfigSettings.ConferenceTable);
            }
        }
        public void Handle(IEventContext context, ConferenceUnpublished evnt)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Update(new { IsPublished = 0 }, new { Id = evnt.AggregateRootId }, ConfigSettings.ConferenceTable);
            }
        }
        public void Handle(IEventContext context, SeatTypeAdded evnt)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Insert(new
                {
                    Id = evnt.SeatTypeId,
                    Name = evnt.SeatTypeInfo.Name,
                    Description = evnt.SeatTypeInfo.Description,
                    Quantity = evnt.Quantity,
                    Price = evnt.SeatTypeInfo.Price,
                    ConferenceInfo_Id = evnt.AggregateRootId,
                }, ConfigSettings.SeatTypeTable);
            }
        }
        public void Handle(IEventContext context, SeatTypeUpdated evnt)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Update(new
                {
                    Name = evnt.SeatTypeInfo.Name,
                    Description = evnt.SeatTypeInfo.Description,
                    Price = evnt.SeatTypeInfo.Price,
                }, new { Id = evnt.SeatTypeId }, ConfigSettings.SeatTypeTable);
            }
        }
        public void Handle(IEventContext context, SeatTypeQuantityChanged evnt)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Update(new
                {
                    Quantity = evnt.Quantity
                }, new { Id = evnt.SeatTypeId }, ConfigSettings.SeatTypeTable);
            }
        }
        public void Handle(IEventContext context, SeatTypeRemoved evnt)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Delete(new { Id = evnt.SeatTypeId }, ConfigSettings.SeatTypeTable);
            }
        }
        public void Handle(IEventContext context, SeatsReserved evnt)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    foreach (var reservationItem in evnt.ReservationItems)
                    {
                        connection.Insert(new
                        {
                            ConferenceId = evnt.AggregateRootId,
                            ReservationId = evnt.ReservationId,
                            SeatTypeId = reservationItem.SeatTypeId,
                            Quantity = reservationItem.Quantity
                        }, ConfigSettings.ReservationItemsTable, transaction);
                        var condition = new { ConferenceId = evnt.AggregateRootId, Id = reservationItem.SeatTypeId };
                        var seatType = connection.QueryList(condition, ConfigSettings.SeatTypeTable, transaction: transaction).Single();
                        connection.Update(
                            new { AvailableQuantity = seatType.AvailableQuantity - reservationItem.Quantity },
                            condition,
                            ConfigSettings.SeatTypeTable, transaction: transaction);
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void Handle(IEventContext context, SeatsReservationCommitted evnt)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    var reservationItems = connection.QueryList(
                        new { ConferenceId = evnt.AggregateRootId, ReservationId = evnt.ReservationId },
                        ConfigSettings.ReservationItemsTable, transaction: transaction);
                    foreach (var reservationItem in reservationItems)
                    {
                        var condition = new { ConferenceId = evnt.AggregateRootId, Id = reservationItem.SeatTypeId };
                        var seatType = connection.QueryList(condition, ConfigSettings.SeatTypeTable, transaction: transaction).Single();
                        connection.Update(
                            new { Quantity = seatType.Quantity - reservationItem.Quantity },
                            condition,
                            ConfigSettings.SeatTypeTable, transaction: transaction);
                    }

                    connection.Delete(new { ConferenceId = evnt.AggregateRootId, ReservationId = evnt.ReservationId }, ConfigSettings.ReservationItemsTable, transaction);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void Handle(IEventContext context, SeatsReservationCancelled evnt)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    var reservationItems = connection.QueryList(
                        new { ConferenceId = evnt.AggregateRootId, ReservationId = evnt.ReservationId },
                        ConfigSettings.ReservationItemsTable, transaction: transaction);
                    foreach (var reservationItem in reservationItems)
                    {
                        var condition = new { ConferenceId = evnt.AggregateRootId, Id = reservationItem.SeatTypeId };
                        var seatType = connection.QueryList(condition, ConfigSettings.SeatTypeTable, transaction: transaction).Single();
                        connection.Update(
                            new { AvailableQuantity = seatType.AvailableQuantity + reservationItem.Quantity },
                            condition,
                            ConfigSettings.SeatTypeTable, transaction: transaction);
                    }

                    connection.Delete(new { ConferenceId = evnt.AggregateRootId, ReservationId = evnt.ReservationId }, ConfigSettings.ReservationItemsTable, transaction);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
