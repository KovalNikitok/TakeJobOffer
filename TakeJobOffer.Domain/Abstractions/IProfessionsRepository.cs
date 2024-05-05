using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions
{
    public interface IProfessionsRepository
    {
        Task<Guid> CreateProfession(Profession profession);
        Task<Guid> CreateProfessionWithSlug(Profession profession, ProfessionSlug professionSlug);
        Task<Guid> DeleteProfession(Guid id);
        Task<Profession?> GetProfessionById(Guid id);
        Task<Profession?> GetProfessionBySlug(string slug);
        Task<List<Profession?>> GetProfessions();
        Task<Guid> UpdateProfession(Guid id, string name, string? description);

    }
}