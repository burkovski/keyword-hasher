using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class KeywordsContext : DbContext, IKeywordsContext
    {
        public KeywordsContext(DbContextOptions<KeywordsContext> options) : base(options)
        { }

        public DbSet<Keyword> Keywords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Keyword>();

            builder.ToTable("seo_query", "legent");

            builder.HasKey(k => new {k.SearchString, k.CountryCode});

            builder.Property(keyword => keyword.CountryCode).HasColumnName("country");
            builder.Property(keyword => keyword.SearchString).HasColumnName("ss");
        }
    }
}
