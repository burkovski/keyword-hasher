using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KeywordHasherJob
{
    internal static class Program
    {
        private static async Task Main()
        {
            IServiceCollection services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var keywordHasher = serviceProvider.GetRequiredService<KeywordHasher>();
            var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();

            using var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            try
            {
                await keywordHasher.GenerateAndStoreHashesForKeywordsAsync(cancellationToken);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Can't generate and store hashes for keyword");
            }
        }
    }
}
