using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeJobOffer.DAL.Entities;

namespace TakeJobOffer.DAL.Configurations
{
    public class ProfessionConfiguration : IEntityTypeConfiguration<ProfessionEntity>
    {
        public void Configure(EntityTypeBuilder<ProfessionEntity> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name).HasMaxLength(255).IsRequired();
            builder.Property(i => i.Description).HasMaxLength(255);
        }
    }
}
