using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions.Services
{
    public interface IProfessionsSkillsService
    {
        Task<List<ProfessionSkill?>?> GetSkillsByProfessionIdAsync(Guid professionId);
        Task<Guid?> CreateSkillForProfessionAsync(ProfessionSkill professionSkill);
        Task<Guid?> UpdateSkillMentionCountForProfessionAsync(Guid professionId, Guid skillId, int skillMentionCount);
        Task<Guid> DeleteSkillForProfessionAsync(Guid professionId, Guid skillId);


    }
}
