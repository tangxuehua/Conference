using System.Net;
using System.Collections.Generic;
using Conference.Common;
using ECommon.Socketing;
using ENode.Configurations;
using ENode.EQueue;
using ENode.Eventing;
using ENode.Infrastructure;
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

        public static ENodeConfiguration BuildContainer(this ENodeConfiguration enodeConfiguration)
        {
            enodeConfiguration.GetCommonConfiguration().BuildContainer();
            return enodeConfiguration;
        }
        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();

            configuration.RegisterEQueueComponents();

            _applicationMessagePublisher = new ApplicationMessagePublisher();
            _domainEventPublisher = new DomainEventPublisher();
            _exceptionPublisher = new PublishableExceptionPublisher();

            configuration.SetDefault<IMessagePublisher<IApplicationMessage>, ApplicationMessagePublisher>(_applicationMessagePublisher);
            configuration.SetDefault<IMessagePublisher<DomainEventStreamMessage>, DomainEventPublisher>(_domainEventPublisher);
            configuration.SetDefault<IMessagePublisher<IPublishableException>, PublishableExceptionPublisher>(_exceptionPublisher);

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
            _exceptionPublisher.InitializeEQueue(producerSetting);
            _applicationMessagePublisher.InitializeEQueue(producerSetting);

            _commandConsumer = new CommandConsumer().InitializeEQueue("ConferenceCommandConsumerGroup", consumerSetting).Subscribe(Topics.ConferenceCommandTopic);
            _eventConsumer = new DomainEventConsumer().InitializeEQueue("ConferenceEventConsumerGroup", consumerSetting).Subscribe(Topics.ConferenceDomainEventTopic);
            _exceptionConsumer = new PublishableExceptionConsumer().InitializeEQueue("ConferenceExceptionConsumerGroup", consumerSetting).Subscribe(Topics.ConferenceExceptionTopic);

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
