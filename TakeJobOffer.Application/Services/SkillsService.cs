using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Application.Services
{
    public class SkillsService(ISkillsRepository skillsRepository) : ISkillsService
    {
        private readonly ISkillsRepository _skillsRepository = skillsRepository;

        public async Task<List<Skill?>> GetAllSkills()
        {
            return await _skillsRepository.GetSkills();
        }

        public async Task<List<Skill?>?> GetSkillsByIds(IEnumerable<Guid> skillsIds)
        {
            return await _skillsRepository.GetSkillsByIds(skillsIds);
        }

        public async Task<Skill?> GetSkillById(Guid id)
        {
            return await _skillsRepository.GetSkillById(id);
        }

        public async Task<Skill?> GetSkillByName(string name)
        {
            return await _skillsRepository.GetSkillByName(name);
        }

        public async Task<Guid> CreateSkill(Skill skill)
        {
            return await _skillsRepository.CreateSkill(skill);
        }

        public async Task<Guid> UpdateSkill(Guid id, string name)
        {
            return await _skillsRepository.UpdateSkill(
                id: id,
                name: name);
        }

        public async Task<Guid> DeleteSkill(Guid id)
        {
            return await _skillsRepository.DeleteSkill(id);
        }
    }
}
