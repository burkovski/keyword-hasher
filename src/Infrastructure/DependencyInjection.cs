using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Services.KeywordHasher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<KeywordsContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("Keywords"))
                        .EnableDetailedErrors(),
                ServiceLifetime.Transient);

            services.AddTransient<IKeywordsContext>(provider => provider.GetService<KeywordsContext>());

            services.AddTransient<IKeywordHasher, KeywordHasherService>();

            return services;
        }
    }
}
