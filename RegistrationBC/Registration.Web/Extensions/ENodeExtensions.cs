using System.Net;
using ECommon.Components;
using ECommon.Utilities;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using ENode.EQueue.Commanding;
using ENode.Infrastructure;
using ENode.Infrastructure.Impl;
using EQueue.Configurations;
using Payments.Commands;
using Registration.Commands.Orders;
using Registration.Commands.SeatAssignments;

namespace Registration.Web.Extensions
{
    public static class ENodeExtensions
    {
        private static CommandService _commandService;

        public static ENodeConfiguration RegisterAllTypeCodes(this ENodeConfiguration enodeConfiguration)
        {
            var provider = ObjectContainer.Resolve<ITypeCodeProvider>() as DefaultTypeCodeProvider;

            //commands
            provider.RegisterType<PlaceOrder>(220);
            provider.RegisterType<ConfirmReservation>(221);
            provider.RegisterType<ConfirmPayment>(222);
            provider.RegisterType<CloseOrder>(223);
            provider.RegisterType<MarkAsSuccess>(224);
            provider.RegisterType<CreateSeatAssignments>(225);
            provider.RegisterType<AssignSeat>(226);
            provider.RegisterType<UnassignSeat>(227);
            provider.RegisterType<AssignRegistrantDetails>(228);

            provider.RegisterType<CreatePayment>(240);
            provider.RegisterType<CompletePayment>(241);
            provider.RegisterType<CancelPayment>(242);

            return enodeConfiguration;
        }
        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();

            configuration.RegisterEQueueComponents();

            _commandService = new CommandService(new CommandResultProcessor(new IPEndPoint(SocketUtils.GetLocalIPV4(), 9001)));

            configuration.SetDefault<ICommandService, CommandService>(_commandService);

            return enodeConfiguration;
        }
        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _commandService.Start();
            return enodeConfiguration;
        }
    }
}