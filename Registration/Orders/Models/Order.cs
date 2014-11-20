using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Utilities;
using ENode.Domain;
using Registration.SeatAssigning;

namespace Registration.Orders
{
    [Serializable]
    public class Order : AggregateRoot<Guid>
    {
        private OrderTotal _total;
        private Guid _conferenceId;
        private bool _isReservationConfirmed;
        private bool _isPaymentConfirmed;
        private RegistrantInfo _registrant;

        public Order(Guid id, Guid conferenceId, IEnumerable<SeatQuantity> seats, IPricingService pricingService) : base(id)
        {
            var orderTotal = pricingService.CalculateTotal(conferenceId, seats);
            ApplyEvent(new OrderPlaced(id, conferenceId, orderTotal, ObjectId.GenerateNewStringId()));
        }

        public void ConfirmReservation()
        {
            if (!_isReservationConfirmed)
            {
                ApplyEvent(new OrderReservationConfirmed(_id, _conferenceId));
            }
        }
        public void ConfirmPayment()
        {
            if (!_isPaymentConfirmed)
            {
                ApplyEvent(new OrderPaymentConfirmed(_id, _conferenceId));
            }
        }
        public void AssignRegistrant(RegistrantInfo registrant)
        {
            if (!_isReservationConfirmed)
            {
                throw new InvalidOperationException("Cannot assign registrant for an order that the reservation isn't confirmed yet.");
            }
            ApplyEvent(new OrderRegistrantAssigned(_id, _conferenceId, registrant));
        }

        public SeatAssignments CreateSeatAssignments()
        {
            if (!_isPaymentConfirmed)
            {
                throw new InvalidOperationException("Cannot create seat assignments for an order that isn't confirmed yet.");
            }
            return new SeatAssignments(Guid.NewGuid(), _id, _total.Lines.Select(x => new SeatQuantity(x.SeatType, x.Quantity)).ToList());
        }

        private void Handle(OrderPlaced e)
        {
            _id = e.AggregateRootId;
            _conferenceId = e.ConferenceId;
            _total = e.Total;
        }
        private void Handle(OrderReservationConfirmed e)
        {
            _isReservationConfirmed = true;
        }
        private void Handle(OrderPaymentConfirmed e)
        {
            _isPaymentConfirmed = true;
        }
        private void Handle(OrderRegistrantAssigned e)
        {
            _registrant = e.Registrant;
        }
    }
}
