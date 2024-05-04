using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Application.Services
{
    public class ProfessionsSlugService(IProfessionsSlugRepository professionsSlugRepository) : IProfessionsSlugService
    {
        private readonly IProfessionsSlugRepository _professionsSlugRepository = professionsSlugRepository;

        public async Task<ProfessionSlug?> GetProfessionSlugById(Guid id)
        {
            return await _professionsSlugRepository.GetProfessionSlugById(id);
        }

        public async Task<ProfessionSlug?> GetProfessionSlugByProfessionId(Guid professionId)
        {
            return await _professionsSlugRepository.GetProfessionSlugByProfessionId(professionId);
        }

        public async Task<Guid> CreateProfessionSlug(ProfessionSlug professionSlug)
        {
            return await _professionsSlugRepository.CreateProfessionSlug(professionSlug);
        }

        public async Task<Guid> UpdateProfessionSlug(Guid id, string slug)
        {
            return await _professionsSlugRepository.UpdateProfessionSlug(id, slug);
        }

        public async Task<Guid> DeleteProfessionSlug(Guid id)
        {
            return await _professionsSlugRepository.DeleteProfessionSlug(id);
        }
    }
}
