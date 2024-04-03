using Microsoft.EntityFrameworkCore;
using TakeJobOffer.DAL.Entities;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.DAL.Repositories
{
    public class ProfessionsSkillsRepository
    {
        private readonly TakeJobOfferDbContext _dbContext;
        public ProfessionsSkillsRepository(TakeJobOfferDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProfessionSkill?>?> GetSkillsByProfessionId(Guid professionId)
        {
            var professionSkillsEntities = await _dbContext.Professions
                .AsNoTracking()
                .Where(p => p.Id == professionId)
                .Select(p => p.ProfessionSkills)
                .SingleOrDefaultAsync();

            var professionSkills = professionSkillsEntities?
                .Select(ps =>
                {
                    var professionSkillEntity = ProfessionSkill.CreateProfessionSkill(
                        professionId,
                        ps.SkillForeignKey,
                        ps.SkillMentionCount);

                    if (professionSkillEntity.IsSuccess)
                        return professionSkillEntity.Value;
                    return null;
                })
                .ToList();

            return professionSkills;
        }

        public async Task<Guid> CreateSkillForProfessionById(ProfessionSkill professionSkill)
        {
            var professionEntity = await _dbContext.Professions
                .AsNoTracking()
                .Where(p => p.Id == professionSkill.ProfessionId)
                .SingleAsync();

            var professionSkillEntity = new ProfessionSkillEntity
            {
                ProfessionForeignKey = professionSkill.ProfessionId,
                SkillForeignKey = professionSkill.SkillId,
                SkillMentionCount = professionSkill.SkillMentionCount
            };

            professionEntity.ProfessionSkills.Add(professionSkillEntity);
            await _dbContext.SaveChangesAsync();

            return professionEntity.Id;
        }


    }
}
