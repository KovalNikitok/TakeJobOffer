using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions
{
    public interface ISkillsService
    {
        Task<List<Skill?>> GetAllSkills();
        Task<List<Skill?>?> GetSkillsByIds(IEnumerable<Guid> skillsIds);
        Task<Skill?> GetSkillById(Guid id);
        Task<Skill?> GetSkillByName(string name);

        Task<Guid> CreateSkill(Skill skill);
        Task<Guid> UpdateSkill(Guid id, string name);
        Task<Guid> DeleteSkill(Guid id);
    }
}
