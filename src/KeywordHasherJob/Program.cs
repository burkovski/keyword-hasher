using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KeywordHasherJob
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var host =  Host.CreateDefaultBuilder(args)
                .ConfigureServices(OnConfigureServices)
                .Build();

            var keywordsHashingJob = host.Services.GetRequiredService<KeywordsHashingJob>();
            var applicationLifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
            await keywordsHashingJob.GenerateAndStoreHashesForKeywordsAsync(applicationLifetime.ApplicationStopped);
        }

        private static void OnConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(hostContext.Configuration);

            services.AddOptions<KeywordsHashingJobSettings>()
                .BindConfiguration("KeywordsHashingJob");
            services.AddTransient<KeywordsHashingJob>();
        }
    }
}
