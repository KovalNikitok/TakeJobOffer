using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Application.Services
{
    public class ProfessionsService : IProfessionsService
    {
        private readonly IProfessionsRepository _professionsRepository;
        public ProfessionsService(IProfessionsRepository professionsRepository)
        {
            _professionsRepository = professionsRepository;
        }

        public async Task<List<Profession?>> GetAllProfessions()
        {
            return await _professionsRepository.GetProfessions();
        }

        public async Task<Guid> CreateProfession(Profession profession)
        {
            return await _professionsRepository.CreateProfession(profession);
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
