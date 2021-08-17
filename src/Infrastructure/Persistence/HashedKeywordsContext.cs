using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class HashedKeywordsContext : DbContext, IHashedKeywordsContext
    {
        public HashedKeywordsContext(DbContextOptions options) : base(options)
        { }

        public DbSet<HashedKeyword> Keywords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<HashedKeyword>();

            builder.ToTable("job_keyword_hash", "dbo");

            builder.HasKey(k => new {k.SearchString, k.CountryCode});

            builder.Property(keyword => keyword.CountryCode).HasColumnName("country_code");
            builder.Property(keyword => keyword.SearchString).HasColumnName("search_string");
            builder.Property(keyword => keyword.Hash).HasColumnName("hash64");
        }
    }
}
