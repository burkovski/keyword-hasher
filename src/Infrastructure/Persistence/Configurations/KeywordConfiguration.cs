using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class KeywordConfiguration : IEntityTypeConfiguration<Keyword>
    {
        public void Configure(EntityTypeBuilder<Keyword> builder)
        {
            builder.ToTable("seo_query", "legent");

            builder.HasKey(k => new { k.Id, k.CountryCode });

            builder.Property(keyword => keyword.Id).HasColumnName("id");
            builder.Property(keyword => keyword.CountryCode).HasColumnName("country");
            builder.Property(keyword => keyword.SearchString).HasColumnName("ss");
            builder.Property(keyword => keyword.Hash).HasColumnName("kw_hash");
        }
    }
}
