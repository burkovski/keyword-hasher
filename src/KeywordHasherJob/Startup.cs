using Application;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KeywordHasherJob
{
    internal class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddLogging(configure => configure
                    .ClearProviders()
                    .AddConsole()
                    .AddConfiguration(Configuration.GetSection("Logging"))
                    .AddFilter("Microsoft", LogLevel.Warning))
                .AddTransient<KeywordHasher>()
                .AddTransient<Startup>();
        }
    }
}
