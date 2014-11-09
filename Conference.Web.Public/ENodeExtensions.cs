using ECommon.Components;
using ECommon.Extensions;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using ENode.EQueue.Commanding;
using ENode.Eventing;
using ENode.Infrastructure;
using EQueue.Broker;
using EQueue.Configurations;

namespace Conference.Web.Public
{
    public static class ENodeExtensions
    {
        private static BrokerController _broker;
        private static CommandService _commandService;
        private static CommandConsumer _commandConsumer;
        private static EventPublisher _eventPublisher;
        private static EventConsumer _eventConsumer;
        private static CommandResultProcessor _commandResultProcessor;

        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();

            configuration.RegisterEQueueComponents();

            _broker = new BrokerController();
            _commandResultProcessor = new CommandResultProcessor();
            _commandService = new CommandService(_commandResultProcessor);
            _eventPublisher = new EventPublisher();

            configuration.SetDefault<ICommandService, CommandService>(_commandService);
            configuration.SetDefault<IMessagePublisher<EventStream>, EventPublisher>(_eventPublisher);
            configuration.SetDefault<IMessagePublisher<DomainEventStream>, EventPublisher>(_eventPublisher);
            configuration.SetDefault<IEventPublisher, EventPublisher>(_eventPublisher);

            _commandConsumer = new CommandConsumer();
            _eventConsumer = new EventConsumer();

            var commandTopics = ObjectContainer.Resolve<ITopicProvider<ICommand>>().GetAllTopics();
            var eventTopics = ObjectContainer.Resolve<ITopicProvider<IEvent>>().GetAllTopics();
            commandTopics.ForEach(topic => _commandConsumer.Subscribe(topic));
            eventTopics.ForEach(topic => _eventConsumer.Subscribe(topic));

            return enodeConfiguration;
        }
        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _broker.Start();
            _eventConsumer.Start();
            _commandConsumer.Start();
            _eventPublisher.Start();
            _commandService.Start();
            _commandResultProcessor.Start();

            return enodeConfiguration;
        }
    }
}