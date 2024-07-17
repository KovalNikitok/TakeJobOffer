using TakeJobOffer.Domain.Abstractions.Repositories;
using TakeJobOffer.Domain.Abstractions.Services;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Application.Services
{
    public class ProfessionsSkillsService(IProfessionsSkillsRepository professionsSkillsRepository) : IProfessionsSkillsService
    {
        private readonly IProfessionsSkillsRepository _professionsSkillsRepository = professionsSkillsRepository;

        public async Task<List<ProfessionSkill?>?> GetSkillsByProfessionIdAsync(Guid professionId)
        {
            return await _professionsSkillsRepository.GetProfessionSkillsAsync(professionId);
        }

        public async Task<Guid?> CreateSkillForProfessionAsync(ProfessionSkill professionSkill)
        {
            return await _professionsSkillsRepository.CreateProfessionSkillAsync(professionSkill);
        }

        public async Task<Guid?> UpdateSkillMentionCountForProfessionAsync(Guid professionId, Guid skillId, int skillMentionCount)
        {
            return await _professionsSkillsRepository.UpdateSkillMentionAsync(professionId, skillId, skillMentionCount);
        }

        public async Task<Guid> DeleteSkillForProfessionAsync(Guid professionId, Guid skillId)
        {
            return await _professionsSkillsRepository.DeleteProfessionSkillAsync(professionId, skillId);
        }
    }
}
