using Conference.Common;
using ConferenceManagement.Messages;
using ECommon.Components;
using ENode.Configurations;
using ENode.EQueue;
using ENode.Eventing;
using ENode.Infrastructure;
using ENode.Infrastructure.Impl;
using EQueue.Configurations;
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
        private static CommandConsumer _commandConsumer;
        private static ApplicationMessageConsumer _applicationMessageConsumer;
        private static DomainEventPublisher _domainEventPublisher;
        private static DomainEventConsumer _eventConsumer;

        public static ENodeConfiguration RegisterAllTypeCodes(this ENodeConfiguration enodeConfiguration)
        {
            var provider = ObjectContainer.Resolve<ITypeCodeProvider>() as DefaultTypeCodeProvider;

            //aggregates
            provider.RegisterType<Order>(120);
            provider.RegisterType<SeatAssignments>(121);

            //commands
            provider.RegisterType<PlaceOrder>(220);
            provider.RegisterType<ConfirmReservation>(221);
            provider.RegisterType<ConfirmPayment>(222);
            provider.RegisterType<CloseOrder>(223);
            provider.RegisterType<MarkAsSuccess>(224);
            provider.RegisterType<CreateSeatAssignments>(225);
            provider.RegisterType<AssignSeat>(226);
            provider.RegisterType<UnassignSeat>(227);

            //application messages
            provider.RegisterType<SeatsReservedMessage>(320);
            provider.RegisterType<SeatInsufficientMessage>(321);
            provider.RegisterType<SeatsReservationCommittedMessage>(322);
            provider.RegisterType<SeatsReservationCancelledMessage>(323);
            provider.RegisterType<PaymentCompletedMessage>(324);
            provider.RegisterType<PaymentRejectedMessage>(325);

            //domain events
            provider.RegisterType<OrderPlaced>(420);
            provider.RegisterType<OrderReservationConfirmed>(421);
            provider.RegisterType<OrderPaymentConfirmed>(422);
            provider.RegisterType<OrderClosed>(423);
            provider.RegisterType<OrderExpired>(424);
            provider.RegisterType<OrderSuccessed>(425);
            provider.RegisterType<SeatAssignmentsCreated>(426);
            provider.RegisterType<SeatAssigned>(427);
            provider.RegisterType<SeatUnassigned>(428);

            //application message, domain event, or exception handlers
            provider.RegisterType<RegistrationProcessManager>(620);
            provider.RegisterType<OrderViewModelGenerator>(621);

            return enodeConfiguration;
        }
        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();

            configuration.RegisterEQueueComponents();

            _domainEventPublisher = new DomainEventPublisher();

            configuration.SetDefault<IMessagePublisher<DomainEventStreamMessage>, DomainEventPublisher>(_domainEventPublisher);

            _commandConsumer = new CommandConsumer().Subscribe(Topics.ConferenceCommandTopic);
            _eventConsumer = new DomainEventConsumer().Subscribe(Topics.ConferenceDomainEventTopic);
            _applicationMessageConsumer = new ApplicationMessageConsumer().Subscribe(Topics.ConferenceApplicationMessageTopic);

            return enodeConfiguration;
        }
        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _applicationMessageConsumer.Start();
            _eventConsumer.Start();
            _commandConsumer.Start();
            _domainEventPublisher.Start();

            return enodeConfiguration;
        }
        public static ENodeConfiguration ShutdownEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _domainEventPublisher.Shutdown();
            _commandConsumer.Shutdown();
            _eventConsumer.Shutdown();
            _applicationMessageConsumer.Shutdown();
            return enodeConfiguration;
        }
    }
}
