using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeJobOffer.DAL.Entities;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.DAL.Configurations
{
    public class ProfessionsSkillsConfiguration : IEntityTypeConfiguration<ProfessionSkillEntity>
    {
        public void Configure(EntityTypeBuilder<ProfessionSkillEntity> builder)
        {
            builder.ToTable("ProfessionsSkills");
            builder.HasKey(ps => new { ps.ProfessionForeignKey, ps.SkillForeignKey });

            builder.Property(ps => ps.SkillMentionCount).IsRequired();
            builder.Property(ps => ps.SkillMentionCount).HasDefaultValue(ProfessionSkill.MIN_PROFESION_SKILL_MENTION);
        }
    }
}
