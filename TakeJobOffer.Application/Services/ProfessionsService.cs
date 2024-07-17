using TakeJobOffer.Domain.Abstractions.Repositories;
using TakeJobOffer.Domain.Abstractions.Services;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Application.Services
{
    public class ProfessionsService(IProfessionsRepository professionsRepository) : IProfessionsService
    {
        private readonly IProfessionsRepository _professionsRepository = professionsRepository;

        public async Task<List<Profession?>> GetProfessionsAsync()
        {
            return await _professionsRepository.GetProfessionsAsync();
        }

        public async Task<Profession?> GetProfessionAsync(Guid id)
        {
            return await _professionsRepository.GetProfessionAsync(id);
        }
        public async Task<Profession?> GetProfessionAsync(string slug)
        {
            return await _professionsRepository.GetProfessionAsync(slug);
        }

        public async Task<Guid> CreateProfessionAsync(Profession profession)
        {
            return await _professionsRepository.CreateProfessionAsync(profession);
        }

        public async Task<Guid> CreateProfessionWithSlugAsync(Profession profession, ProfessionSlug professionSlug)
        {
            return await _professionsRepository.CreateProfessionWithSlugAsync(profession, professionSlug);
        }

        public async Task<Guid> UpdateProfessionAsync(Guid guid, string name, string? description)
        {
            return await _professionsRepository.UpdateProfessionAsync(guid, name, description);
        }

        public async Task<Guid> DeleteProfessionAsync(Guid guid)
        {
            return await _professionsRepository.DeleteProfessionAsync(guid);
        }
    }
}
