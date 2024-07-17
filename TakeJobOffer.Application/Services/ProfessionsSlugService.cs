using TakeJobOffer.Domain.Abstractions.Repositories;
using TakeJobOffer.Domain.Abstractions.Services;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.Application.Services
{
    public class ProfessionsSlugService(IProfessionsSlugRepository professionsSlugRepository) : IProfessionsSlugService
    {
        private readonly IProfessionsSlugRepository _professionsSlugRepository = professionsSlugRepository;

        public async Task<List<ProfessionSlug?>?> GetProfessionSlugsAsync()
        {
            return await _professionsSlugRepository.GetProfessionSlugsAsync();
        }

        public async Task<List<ProfessionSlug?>?> GetProfessionSlugsAsync(IEnumerable<Guid> professionsIds)
        {
            return await _professionsSlugRepository.GetProfessionSlugsAsync(professionsIds);
        }

        public async Task<ProfessionSlug?> GetProfessionSlugAsync(Guid id)
        {
            return await _professionsSlugRepository.GetProfessionSlugAsync(id);
        }

        public async Task<ProfessionSlug?> GetProfessionSlugByProfessionId(Guid professionId)
        {
            return await _professionsSlugRepository.GetProfessionSlugByProfessionIdAsync(professionId);
        }

        public async Task<Guid> CreateProfessionSlugAsync(ProfessionSlug professionSlug)
        {
            return await _professionsSlugRepository.CreateProfessionSlugAsync(professionSlug);
        }

        public async Task<Guid> UpdateProfessionSlugAsync(Guid id, string slug)
        {
            return await _professionsSlugRepository.UpdateProfessionSlugAsync(id, slug);
        }

        public async Task<Guid> DeleteProfessionSlugAsync(Guid id)
        {
            return await _professionsSlugRepository.DeleteProfessionSlugAsync(id);
        }
    }
}
