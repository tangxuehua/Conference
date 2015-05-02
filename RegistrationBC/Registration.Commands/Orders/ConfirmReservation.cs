using System;
using System.Collections.Generic;
using ENode.Commanding;

namespace Registration.Commands.Orders
{
    [Serializable]
    public class ConfirmReservation : Command<Guid>
    {
        public bool IsReservationSuccess { get; set; }

        public ConfirmReservation(Guid orderId, bool isReservationSuccess) : base(orderId)
        {
            IsReservationSuccess = isReservationSuccess;
        }
    }
}
