using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions
{
    public interface ISkillsRepository
    {
        Task<List<Skill?>> GetSkills();
        Task<Guid> CreateSkills(Guid id, string name);
        Task<Guid> UpdateSkills(Guid id, string name);
        Task<Guid> DeleteSkills(Guid id);
    }
}
