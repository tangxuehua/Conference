using System.Collections.Generic;

namespace Registration
{
    public struct OrderTotal
    {
        public ICollection<OrderLine> Lines { get; set; }
        public decimal Total { get; set; }
    }
}
