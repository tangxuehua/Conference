using System;
using System.Collections.Generic;

namespace Registration.Orders
{
    public interface IPricingService
    {
        OrderTotal CalculateTotal(Guid conferenceId, IEnumerable<SeatInfo> seats);
    }
}
