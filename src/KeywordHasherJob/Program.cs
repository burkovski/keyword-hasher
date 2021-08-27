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
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<KeywordsHashingService>();

                    services.AddApplication();
                    services.AddInfrastructure(hostContext.Configuration);

                    services.AddTransient<KeywordsHashingJob>();
                    services.AddOptions<KeywordsHashingJobSettings>()
                        .Bind(hostContext.Configuration.GetSection("KeywordsHashingJob"));
                })
                .RunConsoleAsync();
        }
    }
}
