using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Components;

namespace Registration.Orders
{
    [Component]
    public class DefaultPricingService : IPricingService
    {
        public OrderTotal CalculateTotal(Guid conferenceId, IEnumerable<SeatInfo> seats)
        {
            var orderLines = new List<OrderLine>();
            foreach (var seat in seats)
            {
                orderLines.Add(new OrderLine(seat, Math.Round(seat.UnitPrice * seat.Quantity, 2)));
            }
            return new OrderTotal(orderLines.ToArray(), orderLines.Sum(x => x.LineTotal));
        }
    }
}
