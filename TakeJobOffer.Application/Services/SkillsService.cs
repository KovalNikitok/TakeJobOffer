using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Application.Services
{
    public class SkillsService : ISkillsService
    {
        private readonly ISkillsRepository _skillsRepository;
        public SkillsService(ISkillsRepository skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }
        public async Task<List<Skill?>> GetAllSkills()
        {
            return await _skillsRepository.GetSkills();
        }

        public async Task<Skill?> GetSkillById(Guid id)
        {
            return await _skillsRepository.GetSkillById(id);
        }

        public async Task<Guid> CreateSkill(string name)
        {
            return await _skillsRepository.CreateSkill(
                id: Guid.NewGuid(),
                name: name);
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
