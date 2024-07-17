using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions.Services
{
    public interface ISkillsService
    {
        Task<List<Skill?>> GetSkillsAsync();
        Task<List<Skill?>?> GetSkillsAsync(IEnumerable<Guid> skillsIds);
        Task<Skill?> GetSkillAsync(Guid id);
        Task<Skill?> GetSkillAsync(string name);

        Task<Guid> CreateSkillAsync(Skill skill);
        Task<Guid> UpdateSkillAsync(Guid id, string name);
        Task<Guid> DeleteSkillAsync(Guid id);
    }
}
