using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions.Repositories
{
    public interface IProfessionsSkillsRepository
    {
        Task<List<ProfessionSkill?>?> GetProfessionSkillsAsync(Guid professionId);
        Task<Guid?> CreateProfessionSkillAsync(ProfessionSkill professionSkill);
        Task<Guid?> UpdateSkillMentionAsync(Guid professionId, Guid skillId, int skillMentionCount);
        Task<Guid> DeleteProfessionSkillAsync(Guid professionId, Guid skillId);
    }
}