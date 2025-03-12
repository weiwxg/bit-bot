using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Wei.BitBot.BlockBeats;

namespace Wei.BitBot;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpJsonModule),
    typeof(AbpBackgroundWorkersModule)
)]
public class BitBotModule : AbpModule
{
    public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClient();

        return Task.CompletedTask;
    }

    public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        //var logger = context.ServiceProvider.GetRequiredService<ILogger<BitBotModule>>();
        //var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
        //logger.LogInformation($"MySettingName => {configuration["MySettingName"]}");

        //var hostEnvironment = context.ServiceProvider.GetRequiredService<IHostEnvironment>();
        //logger.LogInformation($"EnvironmentName => {hostEnvironment.EnvironmentName}");

        await context.AddBackgroundWorkerAsync<BlockBeatsFlashWorker>();
    }
}
