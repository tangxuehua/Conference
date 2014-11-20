using System;
using System.Collections.Generic;
using ENode.Commanding;

namespace Registration.Commands
{
    [Serializable]
    public class ConfirmReservation : AggregateCommand<Guid>
    {
        public ConfirmReservation(Guid orderId) : base(orderId) { }
    }
}
