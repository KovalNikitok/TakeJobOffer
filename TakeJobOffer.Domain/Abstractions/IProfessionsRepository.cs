
namespace TakeJobOffer.DAL.Repositories
{
    public interface IProfessionsRepository
    {
        Task<Guid> CreateProfession(Profession profession);
        Task<Guid> DeleteProfession(Guid id);
        Task<Profession> GetProfessions();
        Task<Guid> UpdateProfession(Guid id, string name, string description);
    }
}