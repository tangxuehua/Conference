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

        public static ENodeConfiguration RegisterAllTypeCodes(this ENodeConfiguration enodeConfiguration)
        {
            var provider = ObjectContainer.Resolve<ITypeCodeProvider>() as DefaultTypeCodeProvider;

            //aggregates
            provider.RegisterType<Payment>(130);

            //commands
            provider.RegisterType<CreatePayment>(240);
            provider.RegisterType<CompletePayment>(241);
            provider.RegisterType<CancelPayment>(242);

            //application messages
            provider.RegisterType<PaymentCompletedMessage>(320);
            provider.RegisterType<PaymentRejectedMessage>(321);

            //domain events
            provider.RegisterType<PaymentInitiated>(440);
            provider.RegisterType<PaymentCompleted>(441);
            provider.RegisterType<PaymentRejected>(442);

            //application message, domain event, or exception handlers
            provider.RegisterType<PaymentViewModelGenerator>(640);
            provider.RegisterType<PaymentMessagePublisher>(641);

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
