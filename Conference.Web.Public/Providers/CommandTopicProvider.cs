using ECommon.Components;
using ENode.Commanding;
using ENode.EQueue;
using Payments.Commands;
using Registration.Commands;

namespace Conference.Web.Public.Providers
{
    [Component]
    public class CommandTopicProvider : AbstractTopicProvider<ICommand>
    {
        public CommandTopicProvider()
        {
            RegisterTopic("RegistrationCommandTopic",
                typeof(AddSeats),
                typeof(AssignRegistrantDetails),
                typeof(AssignSeat),
                typeof(CancelSeatReservation),
                typeof(CommitSeatReservation),
                typeof(ConfirmOrder),
                typeof(CreateSeatAssignments),
                typeof(MakeSeatReservation),
                typeof(MarkSeatsAsReserved),
                typeof(RegisterToConference),
                typeof(RejectOrder),
                typeof(RemoveSeats),
                typeof(SeatsAvailabilityCommand),
                typeof(UnassignSeat));
            RegisterTopic("PaymentCommandTopic",
                typeof(CreatePayment),
                typeof(CompletePayment),
                typeof(CancelPayment));
        }
    }
}
