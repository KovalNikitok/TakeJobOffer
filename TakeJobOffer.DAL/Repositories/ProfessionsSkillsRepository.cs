using Microsoft.EntityFrameworkCore;
using TakeJobOffer.DAL.Entities;
using TakeJobOffer.Domain.Models;
using TakeJobOffer.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace TakeJobOffer.DAL.Repositories
{
    public class ProfessionsSkillsRepository(TakeJobOfferDbContext dbContext) : IProfessionsSkillsRepository
    {
        private readonly TakeJobOfferDbContext _dbContext = dbContext;

        public async Task<List<ProfessionSkill?>?> GetSkillsById(Guid professionId)
        {
            var professionSkillsEntities = await _dbContext.Professions
                .AsNoTracking()
                .Where(p => p.Id == professionId)
                .Select(p => p.ProfessionSkills)
                .SingleOrDefaultAsync();

            if (professionSkillsEntities == null || professionSkillsEntities.Count == 0)
                return null;

            var professionSkills = professionSkillsEntities
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
            var professionSkillCheck = await _dbContext.ProfessionsSkills
                    .Where(ps => ps.ProfessionForeignKey == professionSkill.ProfessionId
                        && ps.SkillForeignKey == professionSkill.SkillId)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();

            if (professionSkillCheck is not null)
                return null;

            await using IDbContextTransaction transaction =
                await _dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Snapshot);

            try
            {
                var professionEntityId = await _dbContext.Professions
                .AsNoTracking()
                .Where(p => p.Id == professionSkill.ProfessionId)
                .Select(p => p.Id)
                .SingleOrDefaultAsync();

                if (professionEntityId == Guid.Empty)
                    return null;

                var skillEntityId = await _dbContext.Skills
                    .AsNoTracking()
                    .Where(s => s.Id == professionSkill.SkillId)
                    .Select(s => s.Id)
                    .SingleOrDefaultAsync();

                if (skillEntityId == Guid.Empty)
                    return null;

                var professionSkillEntity = new ProfessionSkillEntity
                {
                    ProfessionForeignKey = professionEntityId,
                    SkillForeignKey = skillEntityId,
                    SkillMentionCount = professionSkill.SkillMentionCount
                };

                await _dbContext.ProfessionsSkills.AddAsync(professionSkillEntity);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync().ConfigureAwait(false);

                return professionEntityId;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task<Guid?> UpdateSkillMentionById(Guid professionId, Guid skillId, int skillMentionCount)
        {
            var updated = await _dbContext.ProfessionsSkills
                .Where(ps => ps.ProfessionForeignKey == professionId && ps.SkillForeignKey == skillId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(
                        ps => ps.SkillMentionCount,
                        ps => skillMentionCount));

            return professionId;
        }

        public async Task<Guid> DeleteSkillById(Guid professionId, Guid skillId)
        {
            var deleted = await _dbContext.ProfessionsSkills
                .Where(ps => ps.ProfessionForeignKey == professionId && ps.SkillForeignKey == skillId)
                .ExecuteDeleteAsync();

            return professionId;
        }


    }
}
