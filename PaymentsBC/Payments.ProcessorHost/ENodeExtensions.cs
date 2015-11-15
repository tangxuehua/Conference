using System.Net;
using Conference.Common;
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
using Payments.Commands;
using Payments.MessagePublishers;
using Payments.Messages;
using Payments.ReadModel;

namespace Payments.ProcessorHost
{
    public static class ENodeExtensions
    {
        private static CommandConsumer _commandConsumer;
        private static ApplicationMessagePublisher _applicationMessagePublisher;
        private static DomainEventPublisher _domainEventPublisher;
        private static DomainEventConsumer _eventConsumer;

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

            configuration.SetDefault<IMessagePublisher<IApplicationMessage>, ApplicationMessagePublisher>(_applicationMessagePublisher);
            configuration.SetDefault<IMessagePublisher<DomainEventStreamMessage>, DomainEventPublisher>(_domainEventPublisher);

            _commandConsumer = new CommandConsumer("PaymentCommandConsumerGroup", consumerSetting).Subscribe(Topics.PaymentCommandTopic);
            _eventConsumer = new DomainEventConsumer("PaymentEventConsumerGroup", consumerSetting).Subscribe(Topics.PaymentDomainEventTopic);

            return enodeConfiguration;
        }
        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _eventConsumer.Start();
            _commandConsumer.Start();
            _applicationMessagePublisher.Start();
            _domainEventPublisher.Start();
            return enodeConfiguration;
        }
        public static ENodeConfiguration ShutdownEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _applicationMessagePublisher.Shutdown();
            _domainEventPublisher.Shutdown();
            _commandConsumer.Shutdown();
            _eventConsumer.Shutdown();
            return enodeConfiguration;
        }
    }
}
