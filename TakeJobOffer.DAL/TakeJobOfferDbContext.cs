using Microsoft.EntityFrameworkCore;
using TakeJobOffer.DAL.Entities;

namespace TakeJobOffer.DAL
{
    public class TakeJobOfferDbContext : DbContext
    {
        public DbSet<ProfessionEntity> Professions { get; set; }
        public DbSet<SkillEntity> Skills { get; set; }
        public TakeJobOfferDbContext(DbContextOptions<TakeJobOfferDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfessionEntity>()
                .HasMany(e => e.Skills)
                .WithMany()
                .UsingEntity<ProfessionsSkillsEntity>();
        }
    }
}
