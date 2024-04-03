using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions
{
    public interface IProfessionsSkillsService
    {
        Task<List<ProfessionSkill?>?> GetSkillsByProfessionId(Guid professionId);
        Task<Guid?> CreateSkillForProfessionById(ProfessionSkill professionSkill);
        Task<Guid> UpdateSkillMentionCountForProfessionById(Guid professionId, Guid skillId, int skillMentionCount);
        Task<Guid> DeleteSkillForProfessionById(Guid professionId, Guid skillId);
        
        
    }
}
