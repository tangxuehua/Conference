using System;
using System.Collections.Generic;

namespace Registration.ReadModel
{
    public class Order
    {
        private IList<OrderLine> _lines = new List<OrderLine>();

        public Guid OrderId { get; set; }
        public Guid ConferenceId { get; set; }
        public string RegistrantEmail { get; set; }
        public string AccessCode { get; set; }
        public decimal Total { get; set; }
        public int Status { get; set; }
        public DateTime ReservationAutoExpiration { get; set; }

        public void SetLines(IList<OrderLine> lines)
        {
            _lines = lines;
        }
        public IEnumerable<OrderLine> GetLines()
        {
            return _lines;
        }
    }
    public class OrderLine
    {
        public Guid OrderId { get; set; }
        public int Position { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }
}
