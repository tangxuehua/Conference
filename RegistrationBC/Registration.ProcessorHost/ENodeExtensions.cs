using Conference.Common;
using ConferenceManagement.Commands;
using ConferenceManagement.Messages;
using ECommon.Components;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using ENode.Eventing;
using ENode.Infrastructure;
using ENode.Infrastructure.Impl;
using EQueue.Clients.Consumers;
using EQueue.Configurations;
using EQueue.Protocols;
using Payments.Messages;
using Registration.Commands.Orders;
using Registration.Commands.SeatAssignments;
using Registration.Handlers;
using Registration.Orders;
using Registration.ProcessManagers;
using Registration.SeatAssigning;

namespace Registration.ProcessorHost
{
    public static class ENodeExtensions
    {
        private static CommandService _commandService;
        private static CommandConsumer _commandConsumer;
        private static ApplicationMessageConsumer _applicationMessageConsumer;
        private static DomainEventPublisher _domainEventPublisher;
        private static DomainEventConsumer _eventConsumer;

        public static ENodeConfiguration RegisterAllTypeCodes(this ENodeConfiguration enodeConfiguration)
        {
            var provider = ObjectContainer.Resolve<ITypeCodeProvider>() as DefaultTypeCodeProvider;

            //aggregates
            provider.RegisterType<Order>(120);
            provider.RegisterType<OrderSeatAssignments>(121);

            //commands
            provider.RegisterType<MakeSeatReservation>(207);
            provider.RegisterType<CommitSeatReservation>(208);
            provider.RegisterType<CancelSeatReservation>(209);

            provider.RegisterType<PlaceOrder>(220);
            provider.RegisterType<ConfirmReservation>(221);
            provider.RegisterType<ConfirmPayment>(222);
            provider.RegisterType<CloseOrder>(223);
            provider.RegisterType<MarkAsSuccess>(224);
            provider.RegisterType<CreateSeatAssignments>(225);
            provider.RegisterType<AssignSeat>(226);
            provider.RegisterType<UnassignSeat>(227);
            provider.RegisterType<AssignRegistrantDetails>(228);

            //application messages
            provider.RegisterType<SeatsReservedMessage>(300);
            provider.RegisterType<SeatInsufficientMessage>(301);
            provider.RegisterType<SeatsReservationCommittedMessage>(302);
            provider.RegisterType<SeatsReservationCancelledMessage>(303);

            provider.RegisterType<PaymentCompletedMessage>(320);
            provider.RegisterType<PaymentRejectedMessage>(321);

            //domain events
            provider.RegisterType<OrderPlaced>(420);
            provider.RegisterType<OrderReservationConfirmed>(421);
            provider.RegisterType<OrderPaymentConfirmed>(422);
            provider.RegisterType<OrderClosed>(423);
            provider.RegisterType<OrderExpired>(424);
            provider.RegisterType<OrderSuccessed>(425);
            provider.RegisterType<OrderSeatAssignmentsCreated>(426);
            provider.RegisterType<SeatAssigned>(427);
            provider.RegisterType<SeatUnassigned>(428);
            provider.RegisterType<OrderRegistrantAssigned>(429);

            //application message, domain event, or exception handlers
            provider.RegisterType<RegistrationProcessManager>(620);
            provider.RegisterType<OrderViewModelGenerator>(621);
            provider.RegisterType<OrderSeatAssignmentsViewModelGenerator>(622);

            return enodeConfiguration;
        }
        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();

            configuration.RegisterEQueueComponents();

            _domainEventPublisher = new DomainEventPublisher();

            configuration.SetDefault<IMessagePublisher<DomainEventStreamMessage>, DomainEventPublisher>(_domainEventPublisher);

            _commandService = new CommandService();

            configuration.SetDefault<ICommandService, CommandService>(_commandService);

            _commandConsumer = new CommandConsumer(
                "RegistrationCommandConsumer",
                "RegistrationCommandConsumerGroup")
            .Subscribe(Topics.RegistrationCommandTopic);

            _eventConsumer = new DomainEventConsumer(
                "RegistrationEventConsumer",
                "RegistrationEventConsumerGroup")
            .Subscribe(Topics.RegistrationDomainEventTopic);

            _applicationMessageConsumer = new ApplicationMessageConsumer(
                "RegistrationMessageConsumer",
                "RegistrationMessageConsumerGroup")
            .Subscribe(Topics.ConferenceApplicationMessageTopic)
            .Subscribe(Topics.PaymentApplicationMessageTopic);

            return enodeConfiguration;
        }
        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _applicationMessageConsumer.Start();
            _eventConsumer.Start();
            _commandConsumer.Start();
            _domainEventPublisher.Start();
            _commandService.Start();

            return enodeConfiguration;
        }
        public static ENodeConfiguration ShutdownEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _commandService.Shutdown();
            _domainEventPublisher.Shutdown();
            _commandConsumer.Shutdown();
            _eventConsumer.Shutdown();
            _applicationMessageConsumer.Shutdown();
            return enodeConfiguration;
        }
    }
}
