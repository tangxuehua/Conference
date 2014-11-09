using ECommon.Components;
using ENode.Commanding;
using ENode.Infrastructure.Impl;
using Payments.Commands;
using Registration.Commands;

namespace Conference.Web.Public.Providers
{
    [Component]
    public class CommandTypeCodeProvider : DefaultTypeCodeProvider<ICommand>
    {
        public CommandTypeCodeProvider()
        {
            RegisterType<AddSeats>(100);
            RegisterType<AssignRegistrantDetails>(101);
            RegisterType<AssignSeat>(102);
            RegisterType<CancelSeatReservation>(103);
            RegisterType<CommitSeatReservation>(104);
            RegisterType<ConfirmOrder>(105);
            RegisterType<CreateSeatAssignments>(106);
            RegisterType<MakeSeatReservation>(107);
            RegisterType<MarkSeatsAsReserved>(108);
            RegisterType<RegisterToConference>(109);
            RegisterType<RejectOrder>(110);
            RegisterType<RemoveSeats>(111);
            RegisterType<SeatsAvailabilityCommand>(112);
            RegisterType<UnassignSeat>(113);

            RegisterType<CreatePayment>(200);
            RegisterType<CompletePayment>(201);
            RegisterType<CancelPayment>(202);
        }
    }
}
