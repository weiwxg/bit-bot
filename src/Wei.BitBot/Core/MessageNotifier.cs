using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Wei.BitBot.Core
{
    public class MessageNotifier : IMessageNotifier, ITransientDependency
    {
        public ILogger<MessageNotifier> Logger { get; set; }

        protected IServiceScopeFactory ServiceScopeFactory { get; }

        public MessageNotifier(IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
            Logger = NullLogger<MessageNotifier>.Instance;
        }

        public async Task NotifyAsync(MessageNotificationBuilder builder)
        {
            using var scope = ServiceScopeFactory.CreateScope();

            var messageSubscribers = scope.ServiceProvider.GetServices<IMessageSubscriber>();

            foreach (var messageSubsriber in messageSubscribers)
            {
                try
                {
                    await messageSubsriber.HandleAsync(builder);
                }
                catch (Exception e)
                {
                    Logger.LogWarning($"Message subscriber of type {messageSubsriber.GetType().AssemblyQualifiedName} has thrown an exception!");
                    Logger.LogException(e, LogLevel.Warning);
                }
            }
        }
    }
}
