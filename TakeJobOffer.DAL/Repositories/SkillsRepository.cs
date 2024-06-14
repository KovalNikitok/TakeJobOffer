using Microsoft.EntityFrameworkCore;
using TakeJobOffer.DAL.Entities;
using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.DAL.Repositories
{
    public class SkillsRepository (TakeJobOfferDbContext dbContext) : ISkillsRepository
    {
        private readonly TakeJobOfferDbContext _dbContext = dbContext;

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

        public async Task<Skill?> GetSkillByName(string name)
        {
            var skillEntity = await _dbContext.Skills
                .AsNoTracking()
                .Where(i => i.Name == name)
                .FirstOrDefaultAsync();

            if(skillEntity == null) 
                return null;

            var skill = Skill.Create(
                skillEntity.Id,
                skillEntity.Name);

            if (skill.IsSuccess)
                return skill.Value;

            return null;
        }

        public async Task<Skill?> GetSkillById(Guid id)
        {
            var skillEntity = await _dbContext.Skills
                .AsNoTracking()
                .Where(i => i.Id == id)
                .SingleOrDefaultAsync();

            if (skillEntity == null)
                return null;

            var skill = Skill.Create(
                id: skillEntity?.Id ?? Guid.Empty,
                name: skillEntity?.Name ?? string.Empty);

            if(skill.IsSuccess) 
                return skill.Value; 
            
            return null;
        }

        public async Task<List<Skill?>?> GetSkillsByIds(IEnumerable<Guid> skillsIds)
        {          

            var skillsEntitiesList = await (from skillsEntities in _dbContext.Skills
                                     join skillId in skillsIds
                                     on skillsEntities.Id equals skillId
                                     select skillsEntities)
                                .ToListAsync();

            if (skillsEntitiesList == null || skillsEntitiesList.Count == 0)
                return null;

            var skillsList = skillsEntitiesList.Select(s => 
            {
                var skillResult = Skill.Create(
                id: s.Id,
                name: s.Name);
                if (skillResult.IsSuccess)
                    return skillResult.Value;

                return null;
            }).ToList();

            return skillsList;
        }

        public async Task<Guid> CreateSkill(Skill skill)
        {
            var skillEntity = new SkillEntity
            {
                Id = skill.Id,
                Name = skill.Name
            };

            await _dbContext.Skills.AddAsync(skillEntity);
            await _dbContext.SaveChangesAsync();

            return skillEntity.Id;
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
