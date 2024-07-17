using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions.Services
{
    public interface IProfessionsSlugService
    {
        Task<ProfessionSlug?> GetProfessionSlugAsync(Guid id);
        Task<ProfessionSlug?> GetProfessionSlugByProfessionId(Guid professionId);
        Task<List<ProfessionSlug?>?> GetProfessionSlugsAsync();
        Task<List<ProfessionSlug?>?> GetProfessionSlugsAsync(IEnumerable<Guid> professionsIds);
        Task<Guid> CreateProfessionSlugAsync(ProfessionSlug professionSlug);
        Task<Guid> UpdateProfessionSlugAsync(Guid id, string slug);
        Task<Guid> DeleteProfessionSlugAsync(Guid id);
    }
}
