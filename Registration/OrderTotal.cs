using System.Collections.Generic;

namespace Registration
{
    public struct OrderTotal
    {
        public OrderLine[] Lines { get; set; }
        public decimal Total { get; set; }
    }
}
