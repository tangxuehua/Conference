using System.Net;
using System.Collections.Generic;
using Conference.Common;
using ECommon.Socketing;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using ENode.Eventing;
using ENode.Infrastructure;
using EQueue.Clients.Consumers;
using EQueue.Clients.Producers;
using EQueue.Configurations;

namespace Registration.ProcessorHost
{
    public static class ENodeExtensions
    {
        private static CommandService _commandService;
        private static CommandConsumer _commandConsumer;
        private static ApplicationMessageConsumer _applicationMessageConsumer;
        private static DomainEventPublisher _domainEventPublisher;
        private static DomainEventConsumer _eventConsumer;

        public static ENodeConfiguration BuildContainer(this ENodeConfiguration enodeConfiguration)
        {
            enodeConfiguration.GetCommonConfiguration().BuildContainer();
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

            return enodeConfiguration;
        }
        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var producerSetting = new ProducerSetting
            {
                NameServerList = new List<IPEndPoint> { new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.NameServerPort) }
            };
            var consumerSetting = new ConsumerSetting
            {
                NameServerList = new List<IPEndPoint> { new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.NameServerPort) }
            };

            _domainEventPublisher.InitializeEQueue(producerSetting);
            _commandService.InitializeEQueue(null, producerSetting);

            _commandConsumer = new CommandConsumer().InitializeEQueue("RegistrationCommandConsumerGroup", consumerSetting).Subscribe(Topics.RegistrationCommandTopic);
            _eventConsumer = new DomainEventConsumer().InitializeEQueue("RegistrationEventConsumerGroup", consumerSetting).Subscribe(Topics.RegistrationDomainEventTopic);
            _applicationMessageConsumer = new ApplicationMessageConsumer().InitializeEQueue("RegistrationMessageConsumerGroup", consumerSetting)
                .Subscribe(Topics.ConferenceApplicationMessageTopic)
                .Subscribe(Topics.PaymentApplicationMessageTopic);

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
