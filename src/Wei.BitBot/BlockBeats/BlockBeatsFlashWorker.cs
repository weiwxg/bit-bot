using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Json;
using Volo.Abp.Threading;
using Wei.BitBot.Core;

namespace Wei.BitBot.BlockBeats
{
    public class BlockBeatsFlashWorker : AsyncPeriodicBackgroundWorkerBase
    {
        private long _timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

        public BlockBeatsFlashWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory)
            : base(timer, serviceScopeFactory)
        {
            Timer.Period = 1000 * 5; // 5 seconds
            Timer.RunOnStart = true;
        }

        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            // Resolve dependencies
            var messageNotifier = workerContext.ServiceProvider.GetRequiredService<IMessageNotifier>();
            var jsonSerilizer = workerContext.ServiceProvider.GetRequiredService<IJsonSerializer>();
            var httpClientFactory = workerContext.ServiceProvider.GetRequiredService<IHttpClientFactory>();

            // Do the work
            using var client = httpClientFactory.CreateClient();
            try
            {
                // 全部快讯（重要）：https://api.blockbeats.cn/v2/newsflash/newestList?start_time={0}&ios=1&detective=-2
                // 链上侦探：https://api.blockbeats.cn/v2/newsflash/newestList?start_time={0}&ios=-2&detective=1

                var response = await client.GetAsync(string.Format("https://api.blockbeats.cn/v2/newsflash/newestList?start_time={0}&ios=1&detective=-2", _timestamp));
                response.EnsureSuccessStatusCode();

                var responseJson = jsonSerilizer.Deserialize<BlockBeatsDetectiveDto>(await response.Content.ReadAsStringAsync());

                if (responseJson!.Code != 0)
                {
                    Logger.LogWarning($"BlockBeats request failed with code - {0} \n {1}", responseJson.Code, responseJson.Msg);
                    return;
                }

                if (responseJson.Data.List.Count > 0)
                {
                    foreach (var item in responseJson.Data.List)
                    {
                        await messageNotifier.NotifyAsync(new BlockBeatsMessageNotificationBuilder { Data = item });
                    }

                    _timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                }

            }
            catch (HttpRequestException e)
            {
                Logger.LogWarning($"BlockBeats request has thrown an exception!");
                Logger.LogException(e, LogLevel.Warning);

                _timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            }
        }
    }
}
