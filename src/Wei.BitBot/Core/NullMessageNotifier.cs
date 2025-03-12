using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.BitBot.Core
{
    public class NullMessageNotifier : IMessageNotifier
    {
        public static NullMessageNotifier Instance { get; } = new NullMessageNotifier();

        private NullMessageNotifier()
        {

        }

        public Task NotifyAsync(MessageNotificationBuilder builder)
        {
            return Task.CompletedTask;
        }
    }
}
