using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions
{
    public interface IProfessionsSlugService
    {
        Task<Guid> CreateProfessionSlug(ProfessionSlug professionSlug);
        Task<Guid> DeleteProfessionSlug(Guid id);
        Task<ProfessionSlug?> GetProfessionSlugById(Guid id);
        Task<ProfessionSlug?> GetProfessionSlugByProfessionId(Guid professionId);
        Task<List<ProfessionSlug?>?> GetProfessionSlugs();
        Task<List<ProfessionSlug?>?> GetProfessionSlugsByProfessionsIds(IEnumerable<Guid> professionsIds);
        Task<Guid> UpdateProfessionSlug(Guid id, string slug);
    }
}
