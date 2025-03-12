using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.BitBot.Core
{
    public interface IMessageSubscriber
    {
        Task HandleAsync(MessageNotificationBuilder builder);
    }
}
