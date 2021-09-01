using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
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
            modelBuilder.ApplyConfiguration(new KeywordConfiguration());
        }
    }
}
