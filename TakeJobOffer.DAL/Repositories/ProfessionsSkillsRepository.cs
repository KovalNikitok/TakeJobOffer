using Microsoft.EntityFrameworkCore;
using TakeJobOffer.DAL.Entities;
using TakeJobOffer.Domain.Models;
using TakeJobOffer.Domain.Abstractions;

namespace TakeJobOffer.DAL.Repositories
{
    public class ProfessionsSkillsRepository(TakeJobOfferDbContext dbContext) : IProfessionsSkillsRepository
    {
        private readonly TakeJobOfferDbContext _dbContext = dbContext;

        public async Task<List<ProfessionSkill?>?> GetSkillsById(Guid professionId)
        {
            var professionSkillsEntity = await _dbContext.Professions
                .AsNoTracking()
                .Where(p => p.Id == professionId)
                .Select(p => p.ProfessionSkills)
                .SingleOrDefaultAsync();

            if (professionSkillsEntity == null)
                return null;

            var professionSkills = professionSkillsEntity
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

        public async Task<Guid?> CreateSkillById(ProfessionSkill professionSkill)
        {
            var professionEntity = await _dbContext.Professions
                .AsNoTracking()
                .Where(p => p.Id == professionSkill.ProfessionId)
                .SingleOrDefaultAsync();

            if (professionEntity == null)
                return null;

            var skillEntity = _dbContext.Skills
                .AsNoTracking()
                .Where(s => s.Id == professionSkill.SkillId)
                .SingleOrDefaultAsync();

            if(skillEntity == null) 
                return null;

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

        public async Task<Guid> UpdateSkillMentionById(Guid professionId, Guid skillId, int skillMentionCount)
        {
            var updated = await _dbContext.Professions
                .Where(p => p.Id == professionId)
                .Select(p => p.ProfessionSkills.SingleOrDefault(ps => ps.SkillForeignKey == skillId))
                .ExecuteUpdateAsync(s => s
                    .SetProperty(ps => ps.SkillMentionCount, skillMentionCount));

            return professionId;
        }

        public async Task<Guid> DeleteSkillById(Guid professionId, Guid skillId)
        {
            var professionSkillEntity = await _dbContext.Professions
                .Where(p => p.Id == professionId)
                .Select(p => p.Skills.SingleOrDefault(s => s.Id == skillId))
                .ExecuteDeleteAsync();

            return professionId;
        }


    }
}
