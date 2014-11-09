using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ECommon.Components;
using Registration.Orders;

namespace Registration.ReadModel
{
    [Component]
    public class PricingService : IPricingService
    {
        private readonly IConferenceDao conferenceDao;

        public PricingService(IConferenceDao conferenceDao)
        {
            this.conferenceDao = conferenceDao;
        }

        public OrderTotal CalculateTotal(Guid conferenceId, IEnumerable<SeatQuantity> seats)
        {
            var seatTypes = this.conferenceDao.GetPublishedSeatTypes(conferenceId);
            var lineItems = new List<OrderLine>();
            foreach (var item in seats)
            {
                var seatType = seatTypes.FirstOrDefault(x => x.Id == item.SeatType);
                if (seatType == null)
                {
                    throw new Exception(string.Format(CultureInfo.InvariantCulture, "Invalid seat type ID '{0}' for conference with ID '{1}'", item.SeatType, conferenceId));
                }
                lineItems.Add(new SeatOrderLine { SeatType = item.SeatType, Quantity = item.Quantity, UnitPrice = seatType.Price, LineTotal = Math.Round(seatType.Price * item.Quantity, 2) });
            }
            return new OrderTotal
            {
                Total = lineItems.Sum(x => x.LineTotal),
                Lines = lineItems.ToArray()
            };
        }
    }
}
