using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wei.BitBot.Core
{
    public abstract class MessageNotificationBuilder
    {
        public abstract StringContent Build();
    }
}
