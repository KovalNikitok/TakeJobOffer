using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Domain.Abstractions
{
    public interface IProfessionsService
    {
        Task<Guid> CreateProfession(Profession profession);
        Task<Guid> DeleteProfession(Guid guid);
        Task<List<Profession?>> GetAllProfessions();
        Task<Guid> UpdateProfession(Guid guid, string name, string description);
    }
}