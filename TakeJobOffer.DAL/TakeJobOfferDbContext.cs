using Microsoft.EntityFrameworkCore;
using TakeJobOffer.DAL.Configurations;
using TakeJobOffer.DAL.Entities;

namespace TakeJobOffer.DAL
{
    public class TakeJobOfferDbContext(DbContextOptions<TakeJobOfferDbContext> options) : DbContext(options)
    {
        public DbSet<ProfessionEntity> Professions { get; set; }
        public DbSet<ProfessionSlugEntity> ProfessionsSlug { get; set; }
        public DbSet<SkillEntity> Skills { get; set; }
        public DbSet<ProfessionSkillEntity> ProfessionsSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var professionConfiguration = new ProfessionConfiguration();
            var skillConfiguration = new SkillConfiguration();
            var professionsSkillsConfiguration = new ProfessionsSkillsConfiguration();
            var professionSlugConfiguration = new ProfessionSlugConfiguration();

            modelBuilder.ApplyConfiguration(skillConfiguration);
            modelBuilder.ApplyConfiguration(professionConfiguration);
            modelBuilder.ApplyConfiguration(professionSlugConfiguration);
            modelBuilder.ApplyConfiguration(professionsSkillsConfiguration);
        }
    }
}
