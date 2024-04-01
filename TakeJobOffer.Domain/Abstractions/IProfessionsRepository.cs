using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions
{
    public interface IProfessionsRepository
    {
        Task<List<Profession?>> GetProfessions();
        Task<Guid> CreateProfession(Profession profession);
        Task<Guid> UpdateProfession(Guid id, string name, string? description);
        Task<Guid> DeleteProfession(Guid id);
        
    }
}