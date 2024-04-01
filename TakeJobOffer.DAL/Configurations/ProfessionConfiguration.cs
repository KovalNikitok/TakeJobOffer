using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeJobOffer.DAL.Entities;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.DAL.Configurations
{
    public class ProfessionConfiguration : IEntityTypeConfiguration<ProfessionEntity>
    {
        public void Configure(EntityTypeBuilder<ProfessionEntity> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name).HasMaxLength(Profession.MAX_NAME_LENGTH).IsRequired();
            builder.Property(i => i.Description).HasMaxLength(Profession.MAX_DESCRIPTION_LENGTH);
        }
    }
}
