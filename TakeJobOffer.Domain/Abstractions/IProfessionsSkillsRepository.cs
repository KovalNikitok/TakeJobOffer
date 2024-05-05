using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions
{
    public interface IProfessionsSkillsRepository
    {
        Task<Guid?> CreateProfessionSkillById(ProfessionSkill professionSkill);
        Task<Guid> DeleteProfessionSkillById(Guid professionId, Guid skillId);
        Task<List<ProfessionSkill?>?> GetProfessionSkillsById(Guid professionId);
        Task<Guid?> UpdateSkillMentionById(Guid professionId, Guid skillId, int skillMentionCount);
    }
}