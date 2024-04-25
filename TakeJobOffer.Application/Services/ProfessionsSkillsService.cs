using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Application.Services
{
    public class ProfessionsSkillsService(IProfessionsSkillsRepository professionsSkillsRepository) : IProfessionsSkillsService
    {
        private readonly IProfessionsSkillsRepository _professionsSkillsRepository = professionsSkillsRepository;

        public async Task<List<ProfessionSkill?>?> GetSkillsByProfessionId(Guid professionId)
        {
            return await _professionsSkillsRepository.GetProfessionSkillsById(professionId);
        }

        public async Task<Guid?> CreateSkillForProfessionById(ProfessionSkill professionSkill)
        {
            return await _professionsSkillsRepository.CreateProfessionSkillById(professionSkill);
        }

        public async Task<Guid?> UpdateSkillMentionCountForProfessionById(Guid professionId, Guid skillId, int skillMentionCount)
        {
            return await _professionsSkillsRepository.UpdateSkillMentionById(professionId, skillId, skillMentionCount);
        }

        public async Task<Guid> DeleteSkillForProfessionById(Guid professionId, Guid skillId)
        {
            return await _professionsSkillsRepository.DeleteProfessionSkillById(professionId, skillId);
        }
    }
}
