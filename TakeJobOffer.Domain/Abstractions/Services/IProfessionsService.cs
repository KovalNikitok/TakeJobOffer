using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions.Services
{
    public interface IProfessionsService
    {
        Task<List<Profession?>> GetProfessionsAsync();
        Task<Profession?> GetProfessionAsync(Guid id);
        Task<Profession?> GetProfessionAsync(string slug);
        Task<Guid> CreateProfessionAsync(Profession profession);
        Task<Guid> CreateProfessionWithSlugAsync(Profession profession, ProfessionSlug professionSlug);
        Task<Guid> UpdateProfessionAsync(Guid guid, string name, string? description);
        Task<Guid> DeleteProfessionAsync(Guid guid);
        

    }
}