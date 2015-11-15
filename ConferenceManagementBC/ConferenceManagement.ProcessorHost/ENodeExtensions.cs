using System.Net;
using Conference.Common;
using ConferenceManagement.Commands;
using ConferenceManagement.MessagePublishers;
using ConferenceManagement.Messages;
using ConferenceManagement.ReadModel;
using ECommon.Components;
using ECommon.Socketing;
using ENode.Configurations;
using ENode.EQueue;
using ENode.Eventing;
using ENode.Infrastructure;
using ENode.Infrastructure.Impl;
using EQueue.Clients.Consumers;
using EQueue.Clients.Producers;
using EQueue.Configurations;

namespace ConferenceManagement.ProcessorHost
{
    public static class ENodeExtensions
    {
        private static CommandConsumer _commandConsumer;
        private static ApplicationMessagePublisher _applicationMessagePublisher;
        private static DomainEventPublisher _domainEventPublisher;
        private static DomainEventConsumer _eventConsumer;
        private static PublishableExceptionPublisher _exceptionPublisher;
        private static PublishableExceptionConsumer _exceptionConsumer;

        public static ENodeConfiguration RegisterAllTypeCodes(this ENodeConfiguration enodeConfiguration)
        {
            var provider = ObjectContainer.Resolve<ITypeCodeProvider>() as DefaultTypeCodeProvider;

            //aggregates
            provider.RegisterType<Conference>(100);

            //commands
            provider.RegisterType<CreateConference>(200);
            provider.RegisterType<UpdateConference>(201);
            provider.RegisterType<AddSeatType>(202);
            provider.RegisterType<UpdateSeatType>(203);
            provider.RegisterType<RemoveSeatType>(204);
            provider.RegisterType<PublishConference>(205);
            provider.RegisterType<UnpublishConference>(206);
            provider.RegisterType<MakeSeatReservation>(207);
            provider.RegisterType<CommitSeatReservation>(208);
            provider.RegisterType<CancelSeatReservation>(209);

            //application messages
            provider.RegisterType<SeatsReservedMessage>(300);
            provider.RegisterType<SeatInsufficientMessage>(301);
            provider.RegisterType<SeatsReservationCommittedMessage>(302);
            provider.RegisterType<SeatsReservationCancelledMessage>(303);

            //domain events
            provider.RegisterType<ConferenceCreated>(400);
            provider.RegisterType<ConferenceUpdated>(401);
            provider.RegisterType<SeatTypeAdded>(402);
            provider.RegisterType<SeatTypeUpdated>(403);
            provider.RegisterType<SeatTypeRemoved>(404);
            provider.RegisterType<SeatTypeQuantityChanged>(405);
            provider.RegisterType<ConferencePublished>(406);
            provider.RegisterType<ConferenceUnpublished>(407);
            provider.RegisterType<SeatsReserved>(408);
            provider.RegisterType<SeatsReservationCommitted>(409);
            provider.RegisterType<SeatsReservationCancelled>(410);

            //publishable exceptions
            provider.RegisterType<SeatInsufficientException>(500);

            //application message, domain event, or exception handlers
            provider.RegisterType<ConferenceViewModelGenerator>(600);
            provider.RegisterType<ConferenceMessagePublisher>(601);

            return enodeConfiguration;
        }
        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();

            configuration.RegisterEQueueComponents();

            var producerSetting = new ProducerSetting
            {
                BrokerAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerProducerPort),
                BrokerAdminAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerAdminPort)
            };
            var consumerSetting = new ConsumerSetting
            {
                BrokerAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerConsumerPort),
                BrokerAdminAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerAdminPort)
            };

            _applicationMessagePublisher = new ApplicationMessagePublisher(producerSetting);
            _domainEventPublisher = new DomainEventPublisher(producerSetting);
            _exceptionPublisher = new PublishableExceptionPublisher(producerSetting);

            configuration.SetDefault<IMessagePublisher<IApplicationMessage>, ApplicationMessagePublisher>(_applicationMessagePublisher);
            configuration.SetDefault<IMessagePublisher<DomainEventStreamMessage>, DomainEventPublisher>(_domainEventPublisher);
            configuration.SetDefault<IMessagePublisher<IPublishableException>, PublishableExceptionPublisher>(_exceptionPublisher);

            _commandConsumer = new CommandConsumer("ConferenceCommandConsumerGroup", consumerSetting).Subscribe(Topics.ConferenceCommandTopic);
            _eventConsumer = new DomainEventConsumer("ConferenceEventConsumerGroup", consumerSetting).Subscribe(Topics.ConferenceDomainEventTopic);
            _exceptionConsumer = new PublishableExceptionConsumer("ConferenceExceptionConsumerGroup", consumerSetting).Subscribe(Topics.ConferenceExceptionTopic);

            return enodeConfiguration;
        }
        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _exceptionConsumer.Start();
            _eventConsumer.Start();
            _commandConsumer.Start();
            _applicationMessagePublisher.Start();
            _domainEventPublisher.Start();
            _exceptionPublisher.Start();

            return enodeConfiguration;
        }
        public static ENodeConfiguration ShutdownEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _applicationMessagePublisher.Shutdown();
            _domainEventPublisher.Shutdown();
            _exceptionPublisher.Shutdown();
            _commandConsumer.Shutdown();
            _eventConsumer.Shutdown();
            _exceptionConsumer.Shutdown();
            return enodeConfiguration;
        }
    }
}
