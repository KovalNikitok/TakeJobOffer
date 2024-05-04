using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeJobOffer.DAL.Entities;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.DAL.Configurations
{
    public class ProfessionSlugConfiguration : IEntityTypeConfiguration<ProfessionSlugEntity>
    {
        public void Configure(EntityTypeBuilder<ProfessionSlugEntity> builder)
        {
            builder.ToTable("ProfessionSlug");
            builder.HasKey(i => i.Id);
            builder.HasIndex(i => i.Slug).IsUnique(true);
            builder.Property(i => i.ProfessionForeignKey).IsRequired();
            builder.Property(i => i.Slug).HasMaxLength(ProfessionSlug.MAX_SLUG_LENGTH).IsRequired();

            builder
                .HasOne(pSlug => pSlug.Profession)
                .WithOne(p => p.ProfessionSlug)
                .HasForeignKey<ProfessionSlugEntity>(p => p.ProfessionForeignKey);
        }
    }
}
