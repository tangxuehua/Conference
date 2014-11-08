using System;
using ENode.Domain;

namespace Payments
{
    [Serializable]
    public class Payment : AggregateRoot<Guid>
    {
        private Guid _orderId;
        private Guid _conferenceId;
        private PaymentState _state;
        private string _description;
        private decimal _totalAmount;

        public Payment(Guid id, Guid orderId, Guid conferenceId, string description, decimal totalAmount) : base(id)
        {
            ApplyEvent(new PaymentInitiated(id, orderId, conferenceId, description, totalAmount));
        }

        public void Complete()
        {
            if (_state != PaymentState.Initiated)
            {
                throw new InvalidOperationException();
            }
            ApplyEvent(new PaymentCompleted(_id, _orderId, _conferenceId));
        }
        public void Cancel()
        {
            if (_state != PaymentState.Initiated)
            {
                throw new InvalidOperationException();
            }
            ApplyEvent(new PaymentRejected(_id, _orderId, _conferenceId));
        }

        private void Handle(PaymentInitiated evnt)
        {
            _id = evnt.AggregateRootId;
            _orderId = evnt.OrderId;
            _conferenceId = evnt.ConferenceId;
            _description = evnt.Description;
            _totalAmount = evnt.TotalAmount;
            _state = PaymentState.Initiated;
        }
        private void Handle(PaymentCompleted evnt)
        {
            _state = PaymentState.Completed;
        }
        private void Handle(PaymentRejected evnt)
        {
            _state = PaymentState.Rejected;
        }
    }
    public enum PaymentState
    {
        Initiated = 0,
        Completed = 1,
        Rejected = 2
    }
}
