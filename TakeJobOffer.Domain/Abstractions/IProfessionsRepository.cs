using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions
{
    public interface IProfessionsRepository
    {
        Task<Guid> CreateProfession(Profession profession);
        Task<Guid> DeleteProfession(Guid id);
        Task<List<Profession?>> GetProfessions();
        Task<Guid> UpdateProfession(Guid id, string name, string? description);
    }
}