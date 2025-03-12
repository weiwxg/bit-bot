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
    [ExposeServices(typeof(IMessageSubscriber))]
    public abstract class MessageSubscriber : IMessageSubscriber, ITransientDependency
    {
        public ILogger<MessageSubscriber> Logger { get; set; }

        protected MessageSubscriber()
        {
            Logger = NullLogger<MessageSubscriber>.Instance;
        }

        public abstract Task HandleAsync(MessageNotificationBuilder builder);
    }
}
