using System;
using System.Collections.Generic;
using ENode.Commanding;
using ENode.Infrastructure;

namespace Registration.Commands.Orders
{
    [Serializable]
    [Code(3101)]
    public class ConfirmReservation : Command<Guid>
    {
        public bool IsReservationSuccess { get; set; }

        public ConfirmReservation() { }
        public ConfirmReservation(Guid orderId, bool isReservationSuccess) : base(orderId)
        {
            IsReservationSuccess = isReservationSuccess;
        }
    }
}
