using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Services;
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
                options.UseSqlServer(configuration.GetConnectionString("Keywords")));

            services.AddDbContext<HashedKeywordsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("HashedKeywords")));

            services.AddScoped<IKeywordsContext>(provider => provider.GetService<KeywordsContext>());
            services.AddScoped<IHashedKeywordsContext>(provider => provider.GetService<HashedKeywordsContext>());

            services.AddTransient<IKeywordHasher, KeywordHasherService>();

            return services;
        }
    }
}
