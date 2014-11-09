namespace Payments.ReadModel
{
    using System;

    public interface IPaymentDao
    {
        Payment GetPayment(Guid paymentId);
    }
}
