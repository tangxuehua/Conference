namespace Registration.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using ENode.Eventing;
    using Registration.Events;
    using Registration.ReadModel;
    using Registration.ReadModel.Implementation;

    public class DraftOrderViewModelGenerator :
        IEventHandler<OrderPlaced>,
        IEventHandler<OrderUpdated>,
        IEventHandler<OrderPartiallyReserved>,
        IEventHandler<OrderReservationCompleted>,
        IEventHandler<OrderRegistrantAssigned>,
        IEventHandler<OrderConfirmed>
    {
        private readonly Func<ConferenceRegistrationDbContext> contextFactory;

        public DraftOrderViewModelGenerator(Func<ConferenceRegistrationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(IEventContext eventContext, OrderPlaced @event)
        {
            using (var context = this.contextFactory.Invoke())
            {
                var dto = new DraftOrder(@event.AggregateRootId, @event.ConferenceId, DraftOrder.States.PendingReservation, @event.Version)
                {
                    AccessCode = @event.AccessCode,
                };
                dto.Lines.AddRange(@event.Seats.Select(seat => new DraftOrderItem(seat.SeatType, seat.Quantity)));

                context.Save(dto);
            }
        }

        public void Handle(IEventContext eventContext, OrderRegistrantAssigned @event)
        {
            using (var context = this.contextFactory.Invoke())
            {
                var dto = context.Find<DraftOrder>(@event.AggregateRootId);
                if (WasNotAlreadyHandled(dto, @event.Version))
                {
                    dto.RegistrantEmail = @event.Email;
                    dto.OrderVersion = @event.Version;
                    context.Save(dto);
                }
            }
        }

        public void Handle(IEventContext eventContext, OrderUpdated @event)
        {
            using (var context = this.contextFactory.Invoke())
            {
                var dto = context.Set<DraftOrder>().Include(o => o.Lines).First(o => o.OrderId == @event.AggregateRootId);
                if (WasNotAlreadyHandled(dto, @event.Version))
                {
                    var linesSet = context.Set<DraftOrderItem>();
                    foreach (var line in dto.Lines.ToArray())
                    {
                        linesSet.Remove(line);
                    }

                    dto.Lines.AddRange(@event.Seats.Select(seat => new DraftOrderItem(seat.SeatType, seat.Quantity)));

                    dto.State = DraftOrder.States.PendingReservation;
                    dto.OrderVersion = @event.Version;

                    context.Save(dto);
                }
            }
        }

        public void Handle(IEventContext eventContext, OrderPartiallyReserved @event)
        {
            this.UpdateReserved(@event.AggregateRootId, DraftOrder.States.PartiallyReserved, @event.Version, @event.Seats);
        }

        public void Handle(IEventContext eventContext, OrderReservationCompleted @event)
        {
            this.UpdateReserved(@event.AggregateRootId, DraftOrder.States.ReservationCompleted, @event.Version, @event.Seats);
        }

        public void Handle(IEventContext eventContext, OrderConfirmed @event)
        {
            using (var context = this.contextFactory.Invoke())
            {
                var dto = context.Find<DraftOrder>(@event.AggregateRootId);
                if (WasNotAlreadyHandled(dto, @event.Version))
                {
                    dto.State = DraftOrder.States.Confirmed;
                    dto.OrderVersion = @event.Version;
                    context.Save(dto);
                }
            }
        }

        private void UpdateReserved(Guid orderId, DraftOrder.States state, int orderVersion, IEnumerable<SeatQuantity> seats)
        {
            using (var context = this.contextFactory.Invoke())
            {
                var dto = context.Set<DraftOrder>().Include(x => x.Lines).First(x => x.OrderId == orderId);
                if (WasNotAlreadyHandled(dto, orderVersion))
                {
                    foreach (var seat in seats)
                    {
                        var item = dto.Lines.Single(x => x.SeatType == seat.SeatType);
                        item.ReservedSeats = seat.Quantity;
                    }

                    dto.State = state;

                    dto.OrderVersion = orderVersion;

                    context.Save(dto);
                }
            }
        }

        private static bool WasNotAlreadyHandled(DraftOrder draftOrder, int eventVersion)
        {
            // This assumes that events will be handled in order, but we might get the same message more than once.
            if (eventVersion > draftOrder.OrderVersion)
            {
                return true;
            }
            else if (eventVersion == draftOrder.OrderVersion)
            {
                Trace.TraceWarning(
                    "Ignoring duplicate draft order update message with version {1} for order id {0}",
                    draftOrder.OrderId,
                    eventVersion);
                return false;
            }
            else
            {
                Trace.TraceWarning(
                    @"An older order update message was received with with version {1} for order id {0}, last known version {2}.
This read model generator has an expectation that the EventBus will deliver messages for the same source in order.",
                    draftOrder.OrderId,
                    eventVersion,
                    draftOrder.OrderVersion);
                return false;
            }
        }
    }
}
