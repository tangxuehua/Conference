using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ConferenceManagement.Commands;
using ECommon.Components;
using ECommon.Utilities;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using ENode.EQueue.Commanding;
using ENode.Infrastructure;
using ENode.Infrastructure.Impl;
using EQueue.Configurations;

namespace ConferenceManagement.Web.Extensions
{
    public static class ENodeExtensions
    {
        private static CommandService _commandService;

        public static async Task TimeoutAfter(this Task task, int millisecondsDelay)
        {
            var timeoutCancellationTokenSource = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(millisecondsDelay, timeoutCancellationTokenSource.Token));
            if (completedTask == task)
            {
                timeoutCancellationTokenSource.Cancel();
            }
            else
            {
                throw new TimeoutException("The operation has timed out.");
            }
        }
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, int millisecondsDelay)
        {
            var timeoutCancellationTokenSource = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(millisecondsDelay, timeoutCancellationTokenSource.Token));
            if (completedTask == task)
            {
                timeoutCancellationTokenSource.Cancel();
                return task.Result;
            }
            else
            {
                throw new TimeoutException("The operation has timed out.");
            }
        }

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

            return enodeConfiguration;
        }
        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();

            configuration.RegisterEQueueComponents();

            _commandService = new CommandService(new CommandResultProcessor(new IPEndPoint(SocketUtils.GetLocalIPV4(), 9000)));

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