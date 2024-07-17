using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions.Repositories
{
    public interface IProfessionsSlugRepository
    {
        Task<ProfessionSlug?> GetProfessionSlugAsync(Guid id);
        Task<ProfessionSlug?> GetProfessionSlugByProfessionIdAsync(Guid professionId);
        Task<List<ProfessionSlug?>?> GetProfessionSlugsAsync();
        Task<List<ProfessionSlug?>?> GetProfessionSlugsAsync(IEnumerable<Guid> professionsIds);
        Task<Guid> CreateProfessionSlugAsync(ProfessionSlug professionSlug);
        Task<Guid> UpdateProfessionSlugAsync(Guid id, string slug);
        Task<Guid> DeleteProfessionSlugAsync(Guid id);
        
    }
}
