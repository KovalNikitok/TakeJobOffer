using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions
{
    public interface IProfessionsService
    {
        Task<List<Profession?>> GetAllProfessions();
        Task<Profession?> GetProfessionById(Guid id);
        Task<Guid> CreateProfession(Profession profession);
        Task<Guid> UpdateProfession(Guid guid, string name, string? description);
        Task<Guid> DeleteProfession(Guid guid);
        
    }
}