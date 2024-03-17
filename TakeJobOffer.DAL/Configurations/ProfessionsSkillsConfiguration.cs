using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeJobOffer.DAL.Entities;

namespace TakeJobOffer.DAL.Configurations
{
    public class ProfessionsSkillsConfiguration : IEntityTypeConfiguration<ProfessionsSkillsEntity>
    {
        public void Configure(EntityTypeBuilder<ProfessionsSkillsEntity> builder)
        {
            builder.Property(b => b.SkillMentionCount).IsRequired();
        }
    }
}
