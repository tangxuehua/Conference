using System;
using System.Collections.Generic;
using System.Linq;
using ENode.Domain;
using Registration.SeatAssigning;

namespace Registration.Orders
{
    [Serializable]
    public class Order : AggregateRoot<Guid>
    {
        private List<SeatQuantity> _seats;
        private bool _isConfirmed;
        private Guid _conferenceId;

        public Order(Guid id, Guid conferenceId, IEnumerable<SeatQuantity> seats, IPricingService pricingService) : base(id)
        {
            var orderTotal = pricingService.CalculateTotal(conferenceId, seats);
            ApplyEvent(new OrderPlaced(id, conferenceId, seats, Guid.NewGuid().ToString()));
            ApplyEvent(new OrderTotalsCalculated(id, orderTotal.Total, orderTotal.Lines, orderTotal.Total == 0m));
        }

        public void MarkAsReserved(IPricingService pricingService, IEnumerable<SeatQuantity> reservedSeats)
        {
            if (this._isConfirmed)
            {
                throw new InvalidOperationException("Cannot modify a confirmed order.");
            }

            var reserved = reservedSeats.ToList();

            // Is there an order item which didn't get an exact reservation?
            if (this._seats.Any(item => item.Quantity != 0 && !reserved.Any(seat => seat.SeatType == item.SeatType && seat.Quantity == item.Quantity)))
            {
                var orderTotal = pricingService.CalculateTotal(this._conferenceId, reserved);
                ApplyEvent(new OrderPartiallyReserved(_id, reserved));
                ApplyEvent(new OrderTotalsCalculated(_id, orderTotal.Total, orderTotal.Lines, orderTotal.Total == 0m));
            }
            else
            {
                ApplyEvent(new OrderPartiallyReserved(_id, reserved));
            }
        }
        public void Confirm()
        {
            ApplyEvent(new OrderConfirmed(Id) { ConferenceId = _conferenceId });
        }
        public void AssignRegistrant(RegistrantInfo registrant)
        {
            ApplyEvent(new OrderRegistrantAssigned(_id, registrant));
        }

        public SeatAssignments CreateSeatAssignments()
        {
            if (!this._isConfirmed)
                throw new InvalidOperationException("Cannot create seat assignments for an order that isn't confirmed yet.");

            return new SeatAssignments(Guid.NewGuid(), this.Id, this._seats.AsReadOnly());
        }

        private void Handle(OrderPlaced e)
        {
            this._id = e.AggregateRootId;
            this._conferenceId = e.ConferenceId;
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
