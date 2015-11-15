using System.Net;
using Conference.Common;
using ConferenceManagement.Commands;
using ConferenceManagement.ReadModel;
using ECommon.Components;
using ECommon.Socketing;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using ENode.EQueue.Commanding;
using ENode.Infrastructure;
using ENode.Infrastructure.Impl;
using EQueue.Clients.Producers;
using EQueue.Configurations;

namespace ConferenceManagement.Web.Extensions
{
    public static class ENodeExtensions
    {
        private static CommandService _commandService;

        public static ENodeConfiguration RegisterAllTypeCodes(this ENodeConfiguration enodeConfiguration)
        {
            var provider = ObjectContainer.Resolve<ITypeCodeProvider>() as DefaultTypeCodeProvider;

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

            //application message, domain event, or exception handlers
            provider.RegisterType<ConferenceViewModelGenerator>(600);

            return enodeConfiguration;
        }
        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();

            configuration.RegisterEQueueComponents();

            _commandService = new CommandService(new CommandResultProcessor(new IPEndPoint(SocketUtils.GetLocalIPV4(), 9000)), new ProducerSetting
            {
                BrokerAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerProducerPort),
                BrokerAdminAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerAdminPort)
            });

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