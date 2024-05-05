using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Application.Services
{
    public class ProfessionsService(IProfessionsRepository professionsRepository) : IProfessionsService
    {
        private readonly IProfessionsRepository _professionsRepository = professionsRepository;

        public async Task<List<Profession?>> GetAllProfessions()
        {
            return await _professionsRepository.GetProfessions();
        }

        public async Task<Profession?> GetProfessionById(Guid id)
        {
            return await _professionsRepository.GetProfessionById(id);
        }
        public async Task<Profession?> GetProfessionBySlug(string slug)
        {
            return await _professionsRepository.GetProfessionBySlug(slug);
        }

        public async Task<Guid> CreateProfession(Profession profession)
        {
            return await _professionsRepository.CreateProfession(profession);
        }

        public async Task<Guid> CreateProfessionWithSlug(Profession profession, ProfessionSlug professionSlug)
        {
            return await _professionsRepository.CreateProfessionWithSlug(profession, professionSlug);
        }

        public async Task<Guid> UpdateProfession(Guid guid, string name, string? description)
        {
            return await _professionsRepository.UpdateProfession(guid, name, description);
        }

        public async Task<Guid> DeleteProfession(Guid guid)
        {
            return await _professionsRepository.DeleteProfession(guid);
        }
    }
}
