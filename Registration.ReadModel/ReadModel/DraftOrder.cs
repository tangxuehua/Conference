namespace Registration.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class DraftOrder
    {
        private IList<DraftOrderItem> _lines = new List<DraftOrderItem>();
        public enum States
        {
            PendingReservation = 0,
            PartiallyReserved = 1,
            ReservationCompleted = 2,
            Rejected = 3,
            Confirmed = 4,
        }

        public DraftOrder(Guid orderId, Guid conferenceId, States state, int orderVersion = 0) : this()
        {
            this.OrderId = orderId;
            this.ConferenceId = conferenceId;
            this.StateValue = state;
            this.OrderVersion = orderVersion;
        }
        public DraftOrder() { }

        public Guid OrderId { get; set; }
        public Guid ConferenceId { get; set; }
        public DateTime? ReservationExpirationDate { get; set; }
        public States StateValue { get; set; }
        public int OrderVersion { get; set; }
        public string RegistrantEmail { get; set; }
        public string AccessCode { get; set; }

        public void SetLines(IList<DraftOrderItem> lines)
        {
            _lines = lines;
        }
        public IEnumerable<DraftOrderItem> GetLines()
        {
            return _lines;
        }
    }
}
