using System;
using System.Collections.Generic;
using Registration.Orders;

namespace Registration
{
    public interface IPricingService
    {
        OrderTotal CalculateTotal(Guid conferenceId, IEnumerable<SeatQuantity> seats);
    }
}
