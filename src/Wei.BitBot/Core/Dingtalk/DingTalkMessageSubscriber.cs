using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Wei.BitBot.Core.Dingtalk
{
    public class DingtalkMessageSubscriber : MessageSubscriber
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public DingtalkMessageSubscriber(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public override async Task HandleAsync(MessageNotificationBuilder builder)
        {
            using var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync(GetWebhookUri(_configuration["Dingtalk:Webhook"]!, _configuration["Dingtalk:Secret"]!), builder.Build());

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogWarning("Failed to send dingtalk message, status code: {0}", response.StatusCode);
            }
        }

        private string GetWebhookUri(string webhook, string secret)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var sign = GenerateSign(secret, timestamp);
            return $"{webhook}&timestamp={timestamp}&sign={sign}";
        }

        private string GenerateSign(string secret, long timestamp)
        {
            var stringToSign = $"{timestamp}\n{secret}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign));
            return Convert.ToBase64String(hash);
        }
    }
}
