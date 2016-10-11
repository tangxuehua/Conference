using System.Net;
using System.Collections.Generic;
using Conference.Common;
using ECommon.Components;
using ECommon.Socketing;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using ENode.Infrastructure;
using ENode.Infrastructure.Impl;
using EQueue.Clients.Producers;
using EQueue.Configurations;
using Payments.Commands;
using Payments.ReadModel;
using Registration.Commands.Orders;
using Registration.Commands.SeatAssignments;
using Registration.Handlers;

namespace Registration.Web.Extensions
{
    public static class ENodeExtensions
    {
        private static CommandService _commandService;

        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();

            configuration.RegisterEQueueComponents();

            _commandService = new CommandService(new CommandResultProcessor(new IPEndPoint(SocketUtils.GetLocalIPV4(), 9001)), new ProducerSetting
            {
                NameServerList = new List<IPEndPoint> { new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.NameServerPort) }
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