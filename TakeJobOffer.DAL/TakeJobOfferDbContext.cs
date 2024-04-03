using Microsoft.EntityFrameworkCore;
using TakeJobOffer.DAL.Configurations;
using TakeJobOffer.DAL.Entities;

namespace TakeJobOffer.DAL
{
    public class TakeJobOfferDbContext(DbContextOptions<TakeJobOfferDbContext> options) : DbContext(options)
    {
        public DbSet<ProfessionEntity> Professions { get; set; }
        public DbSet<SkillEntity> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var professionConfiguration = new ProfessionConfiguration();
            var skillConfiguration = new SkillConfiguration();
            var professionsSkillsConfiguration = new ProfessionsSkillsConfiguration();

            modelBuilder.ApplyConfiguration(skillConfiguration);
            modelBuilder.ApplyConfiguration(professionConfiguration);
            modelBuilder.ApplyConfiguration(professionsSkillsConfiguration);
        }
    }
}
