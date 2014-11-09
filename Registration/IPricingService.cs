using System;
using System.Collections.Generic;

namespace Registration
{
    public interface IPricingService
    {
        OrderTotal CalculateTotal(Guid conferenceId, IEnumerable<SeatQuantity> seats);
    }
}
