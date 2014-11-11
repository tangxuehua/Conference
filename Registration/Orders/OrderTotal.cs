using System;
using System.Collections.Generic;

namespace Registration.Orders
{
    [Serializable]
    public struct OrderTotal
    {
        public OrderLine[] Lines { get; set; }
        public decimal Total { get; set; }
    }
}
