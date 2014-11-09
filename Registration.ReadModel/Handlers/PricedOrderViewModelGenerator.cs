namespace Registration.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics;
    using System.Linq;
    using ECommon.Components;
    using ENode.Eventing;
    using Registration.Orders;
    using Registration.ReadModel;
    using Registration.ReadModel.Implementation;
    using Registration.SeatAssigning;

    [Component]
    public class PricedOrderViewModelGenerator :
        IEventHandler<OrderPlaced>,
        IEventHandler<OrderTotalsCalculated>,
        IEventHandler<OrderConfirmed>,
        IEventHandler<SeatAssignmentsCreated>
    {
        private readonly Func<ConferenceRegistrationDbContext> contextFactory;

        public PricedOrderViewModelGenerator(Func<ConferenceRegistrationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(IEventContext eventContext, OrderPlaced @event)
        {
            using (var context = this.contextFactory.Invoke())
            {
                var dto = new PricedOrder
                {
                    OrderId = @event.AggregateRootId,
                    OrderVersion = @event.Version
                };
                context.Set<PricedOrder>().Add(dto);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    Trace.TraceWarning(
                        "Ignoring OrderPlaced message with version {1} for order id {0}. This could be caused because the message was already handled and the PricedOrder entity was already created.",
                        dto.OrderId,
                        @event.Version);
                }
            }
        }

        public void Handle(IEventContext eventContext, OrderTotalsCalculated @event)
        {
            var seatTypeIds = @event.Lines.OfType<SeatOrderLine>().Select(x => x.SeatType).Distinct().ToArray();
            using (var context = this.contextFactory.Invoke())
            {
                var dto = context.Query<PricedOrder>().Include(x => x.Lines).First(x => x.OrderId == @event.AggregateRootId);
                if (!WasNotAlreadyHandled(dto, @event.Version))
                {
                    // message already handled, skip.
                    return;
                }

                var linesSet = context.Set<PricedOrderLine>();
                foreach (var line in dto.Lines.ToList())
                {
                    linesSet.Remove(line);
                }

                var seatTypeDescriptions = GetSeatTypeDescriptions(seatTypeIds, context);

                for (int i = 0; i < @event.Lines.Length; i++)
                {
                    var orderLine = @event.Lines[i];
                    var line = new PricedOrderLine
                    {
                        LineTotal = orderLine.LineTotal,
                        Position = i,
                    };

                    var seatOrderLine = orderLine as SeatOrderLine;
                    if (seatOrderLine != null)
                    {
                        // should we update the view model to avoid losing the SeatTypeId?
                        line.Description = seatTypeDescriptions.Where(x => x.SeatTypeId == seatOrderLine.SeatType).Select(x => x.Name).FirstOrDefault();
                        line.UnitPrice = seatOrderLine.UnitPrice;
                        line.Quantity = seatOrderLine.Quantity;
                    }

                    dto.Lines.Add(line);
                }

                dto.Total = @event.Total;
                dto.IsFreeOfCharge = @event.IsFreeOfCharge;
                dto.OrderVersion = @event.Version;

                context.SaveChanges();
            }
        }

        public void Handle(IEventContext eventContext, OrderConfirmed @event)
        {
            using (var context = this.contextFactory.Invoke())
            {
                var dto = context.Find<PricedOrder>(@event.AggregateRootId);
                if (WasNotAlreadyHandled(dto, @event.Version))
                {
                    dto.ReservationExpirationDate = null;
                    dto.OrderVersion = @event.Version;
                    context.Save(dto);
                }
            }
        }

        /// <summary>
        /// Saves the seat assignments correlation ID for further lookup.
        /// </summary>
        public void Handle(IEventContext eventContext, SeatAssignmentsCreated @event)
        {
            using (var context = this.contextFactory.Invoke())
            {
                var dto = context.Find<PricedOrder>(@event.OrderId);
                dto.AssignmentsId = @event.AggregateRootId;
                // Note: @event.Version does not correspond to order.Version.;
                context.SaveChanges();
            }
        }

        private static bool WasNotAlreadyHandled(PricedOrder pricedOrder, int eventVersion)
        {
            // This assumes that events will be handled in order, but we might get the same message more than once.
            if (eventVersion > pricedOrder.OrderVersion)
            {
                return true;
            }
            else if (eventVersion == pricedOrder.OrderVersion)
            {
                Trace.TraceWarning(
                    "Ignoring duplicate priced order update message with version {1} for order id {0}",
                    pricedOrder.OrderId,
                    eventVersion);
                return false;
            }
            else
            {
                Trace.TraceWarning(
                    @"Ignoring an older order update message was received with with version {1} for order id {0}, last known version {2}.
This read model generator has an expectation that the EventBus will deliver messages for the same source in order. Nevertheless, this warning can be expected in a migration scenario.",
                    pricedOrder.OrderId,
                    eventVersion,
                    pricedOrder.OrderVersion);
                return false;
            }
        } 

        private List<PricedOrderLineSeatTypeDescription> GetSeatTypeDescriptions(IEnumerable<Guid> seatTypeIds, ConferenceRegistrationDbContext context)
        {
            return context.Query<PricedOrderLineSeatTypeDescription>().Where(x => seatTypeIds.Contains(x.SeatTypeId)).ToList();
        }
    }
}
