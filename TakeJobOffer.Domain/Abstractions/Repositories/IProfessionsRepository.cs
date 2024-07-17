using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions.Repositories
{
    public interface IProfessionsRepository
    {
        Task<Profession?> GetProfessionAsync(Guid id);
        Task<Profession?> GetProfessionAsync(string slug);
        Task<List<Profession?>> GetProfessionsAsync();
        Task<Guid> CreateProfessionAsync(Profession profession);
        Task<Guid> CreateProfessionWithSlugAsync(Profession profession, ProfessionSlug professionSlug);
        Task<Guid> UpdateProfessionAsync(Guid id, string name, string? description);
        Task<Guid> DeleteProfessionAsync(Guid id);
        

    }
}