using System.Collections.Generic;

namespace Registration.Orders
{
    public struct OrderTotal
    {
        public OrderLine[] Lines { get; set; }
        public decimal Total { get; set; }
    }
}
