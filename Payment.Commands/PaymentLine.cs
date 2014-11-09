using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payment.Commands
{
    public class PaymentLine
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
