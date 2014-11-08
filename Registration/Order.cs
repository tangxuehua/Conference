using System;
using System.Collections.Generic;
using System.Linq;
using ENode.Domain;
using Registration.Events;

namespace Registration
{
    [Serializable]
    public class Order : AggregateRoot<Guid>
    {
        private List<SeatQuantity> _seats;
        private bool _isConfirmed;
        private Guid _conferenceId;

        public Order(Guid id, Guid conferenceId, IEnumerable<OrderItem> items, IPricingService pricingService) : base(id)
        {
            var all = ConvertItems(items);
            var totals = pricingService.CalculateTotal(conferenceId, all.AsReadOnly());

            ApplyEvent(new OrderPlaced(id)
            {
                ConferenceId = conferenceId,
                Seats = all,
                AccessCode = Guid.NewGuid().ToString()
            });
            ApplyEvent(new OrderTotalsCalculated(id) { Total = totals.Total, Lines = totals.Lines != null ? totals.Lines.ToArray() : null, IsFreeOfCharge = totals.Total == 0m });
        }

        public void UpdateSeats(IEnumerable<OrderItem> items, IPricingService pricingService)
        {
            var all = ConvertItems(items);
            var totals = pricingService.CalculateTotal(this._conferenceId, all.AsReadOnly());

            ApplyEvent(new OrderUpdated(Id) { ConferenceId = _conferenceId, Seats = all });
            ApplyEvent(new OrderTotalsCalculated(Id) { Total = totals.Total, Lines = totals.Lines != null ? totals.Lines.ToArray() : null, IsFreeOfCharge = totals.Total == 0m });
        }
        public void MarkAsReserved(IPricingService pricingService, IEnumerable<SeatQuantity> reservedSeats)
        {
            if (this._isConfirmed)
                throw new InvalidOperationException("Cannot modify a confirmed order.");

            var reserved = reservedSeats.ToList();

            // Is there an order item which didn't get an exact reservation?
            if (this._seats.Any(item => item.Quantity != 0 && !reserved.Any(seat => seat.SeatType == item.SeatType && seat.Quantity == item.Quantity)))
            {
                var totals = pricingService.CalculateTotal(this._conferenceId, reserved.AsReadOnly());

                ApplyEvent(new OrderPartiallyReserved(Id) { Seats = reserved.ToArray() });
                ApplyEvent(new OrderTotalsCalculated(Id) { Total = totals.Total, Lines = totals.Lines != null ? totals.Lines.ToArray() : null, IsFreeOfCharge = totals.Total == 0m });
            }
            else
            {
                ApplyEvent(new OrderReservationCompleted(Id) { Seats = reserved.ToArray() });
            }
        }
        public void Confirm()
        {
            ApplyEvent(new OrderConfirmed(Id) { ConferenceId = _conferenceId });
        }
        public void AssignRegistrant(string firstName, string lastName, string email)
        {
            ApplyEvent(new OrderRegistrantAssigned(Id)
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
            });
        }

        public SeatAssignments CreateSeatAssignments()
        {
            if (!this._isConfirmed)
                throw new InvalidOperationException("Cannot create seat assignments for an order that isn't confirmed yet.");

            return new SeatAssignments(Guid.NewGuid(), this.Id, this._seats.AsReadOnly());
        }

        private static List<SeatQuantity> ConvertItems(IEnumerable<OrderItem> items)
        {
            return items.Select(x => new SeatQuantity(x.SeatType, x.Quantity)).ToList();
        }

        private void Handle(OrderPlaced e)
        {
            this._id = e.AggregateRootId;
            this._conferenceId = e.ConferenceId;
            this._seats = e.Seats.ToList();
        }
        private void Handle(OrderUpdated e)
        {
            this._seats = e.Seats.ToList();
        }
        private void Handle(OrderPartiallyReserved e)
        {
            this._seats = e.Seats.ToList();
        }
        private void Handle(OrderReservationCompleted e)
        {
            this._seats = e.Seats.ToList();
        }
        private void Handle(OrderConfirmed e)
        {
            this._isConfirmed = true;
        }
        private void Handle(OrderRegistrantAssigned e)
        {
        }
        private void Handle(OrderTotalsCalculated e)
        {
        }
    }
}
