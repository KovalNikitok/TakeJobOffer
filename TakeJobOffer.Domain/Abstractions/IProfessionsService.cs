using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions
{
    public interface IProfessionsService
    {
        Task<Guid> CreateProfession(Profession profession);
        Task<Guid> CreateProfessionWithSlug(Profession profession, ProfessionSlug professionSlug);
        Task<Guid> DeleteProfession(Guid guid);
        Task<List<Profession?>> GetAllProfessions();
        Task<Profession?> GetProfessionById(Guid id);
        Task<Profession?> GetProfessionBySlug(string slug);
        Task<Guid> UpdateProfession(Guid guid, string name, string? description);

    }
}