using Microsoft.EntityFrameworkCore;
using TakeJobOffer.DAL.Entities;
using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.DAL.Repositories
{
    public class SkillsRepository : ISkillsRepository
    {
        private readonly TakeJobOfferDbContext _dbContext;
        public SkillsRepository(TakeJobOfferDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Skill?>> GetSkills()
        {
            var skillsEntities = await _dbContext.Skills
                .AsNoTracking()
                .ToListAsync();

            var skills = skillsEntities.Select(i =>
                {
                    var skill = Skill.Create(i.Id, i.Name);
                    if (skill.IsSuccess)
                        return skill.Value;
                    return null;
                })
                .ToList();

            return skills;
        }

        public async Task<Skill?> GetSkillById(Guid id)
        {
            var skillEntity = await _dbContext.Skills
                .AsNoTracking()
                .Where(i => i.Id == id)
                .SingleOrDefaultAsync();

            var skill = Skill.Create(
                id: skillEntity?.Id ?? Guid.Empty,
                name: skillEntity?.Name ?? string.Empty);

            if(skill.IsSuccess) 
                return skill.Value; 
            
            return null;
        }

        public async Task<Guid> CreateSkill(Guid id, string name)
        {
            var skill = new SkillEntity
            {
                Id = id,
                Name = name
            };

            await _dbContext.AddAsync(skill);
            await _dbContext.SaveChangesAsync();

            return skill.Id;
        }

        public async Task<Guid> UpdateSkill(Guid id, string name)
        {
            var skill = await _dbContext.Skills
                .Where(i => i.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(i => i.Name, i => name));
        
            return id;
        }

        public async Task<Guid> DeleteSkill(Guid id)
        {
            await _dbContext.Skills
                .Where(i => i.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
