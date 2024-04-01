using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions
{
    public interface ISkillsRepository
    {
        Task<List<Skill?>> GetSkills();
        Task<Skill?> GetSkillById(Guid id);

        Task<Guid> CreateSkill(Skill skill);
        Task<Guid> UpdateSkill(Guid id, string name);
        Task<Guid> DeleteSkill(Guid id);
    }
}
