using TakeJobOffer.Domain.Abstractions.Repositories;
using TakeJobOffer.Domain.Abstractions.Services;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Application.Services
{
    public class SkillsService(ISkillsRepository skillsRepository) : ISkillsService
    {
        private readonly ISkillsRepository _skillsRepository = skillsRepository;

        public async Task<List<Skill?>> GetSkillsAsync()
        {
            return await _skillsRepository.GetSkillsAsync();
        }

        public async Task<List<Skill?>?> GetSkillsAsync(IEnumerable<Guid> skillsIds)
        {
            return await _skillsRepository.GetSkillsAsync(skillsIds);
        }

        public async Task<Skill?> GetSkillAsync(Guid id)
        {
            return await _skillsRepository.GetSkillAsync(id);
        }

        public async Task<Skill?> GetSkillAsync(string name)
        {
            return await _skillsRepository.GetSkillAsync(name);
        }

        public async Task<Guid> CreateSkillAsync(Skill skill)
        {
            return await _skillsRepository.CreateSkillAsync(skill);
        }

        public async Task<Guid> UpdateSkillAsync(Guid id, string name)
        {
            return await _skillsRepository.UpdateSkillAsync(
                id: id,
                name: name);
        }

        public async Task<Guid> DeleteSkillAsync(Guid id)
        {
            return await _skillsRepository.DeleteSkillAsync(id);
        }
    }
}
