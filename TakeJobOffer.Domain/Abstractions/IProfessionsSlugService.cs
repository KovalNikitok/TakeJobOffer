using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions
{
    public interface IProfessionsSlugService
    {
        Task<ProfessionSlug?> GetProfessionSlugById(Guid id);
        Task<ProfessionSlug?> GetProfessionSlugByProfessionId(Guid professionId);
        Task<Guid> CreateProfessionSlug(ProfessionSlug professionSlug);
        Task<Guid> UpdateProfessionSlug(Guid id, string slug);
        Task<Guid> DeleteProfessionSlug(Guid id);
    }
}
