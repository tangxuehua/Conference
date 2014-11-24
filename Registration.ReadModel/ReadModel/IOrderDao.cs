using System;

namespace Registration.ReadModel
{
    public interface IOrderDao
    {
        Order FindOrder(Guid orderId);
        Guid? LocateOrder(string email, string accessCode);
        SeatAssignment[] FindOrderSeatAssignments(Guid orderId);
    }
}