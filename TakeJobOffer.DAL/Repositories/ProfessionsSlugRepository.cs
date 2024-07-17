using Microsoft.EntityFrameworkCore;
using TakeJobOffer.DAL.Entities;
using TakeJobOffer.Domain.Abstractions.Repositories;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.DAL.Repositories
{
    public class ProfessionsSlugRepository(TakeJobOfferDbContext dbContext) : IProfessionsSlugRepository
    {
        private readonly TakeJobOfferDbContext _dbContext = dbContext;

        public async Task<List<ProfessionSlug?>?> GetProfessionSlugsAsync()
        {
            var professionSlugEntities = await _dbContext.ProfessionsSlug
                .AsNoTracking()
                .ToListAsync();

            if (professionSlugEntities == null || professionSlugEntities.Count == 0)
                return null;

            var professionsSlugResult = professionSlugEntities.Select(ps =>
            {
                var psResult = ProfessionSlug.CreateProfessionSlug(
                    ps.Id,
                    ps.ProfessionForeignKey,
                    ps.Slug);
                if (psResult.IsFailed)
                    return null;

                return psResult.Value;
            }).ToList();

            if (professionsSlugResult.Count == 0)
                return null;

            return professionsSlugResult;
        }

        public async Task<List<ProfessionSlug?>?> GetProfessionSlugsAsync(IEnumerable<Guid> professionsIds)
        {
            var professionSlugEntities = await (
                    from professionsSlugEntity in _dbContext.ProfessionsSlug
                    join professionsId in professionsIds
                    on professionsSlugEntity.ProfessionForeignKey equals professionsId
                    select professionsSlugEntity)
                .AsNoTracking()
                .ToListAsync();

            if (professionSlugEntities == null || professionSlugEntities.Count == 0)
                return null;

            var professionsSlugResult = professionSlugEntities.Select(ps =>
            {
                var psResult = ProfessionSlug.CreateProfessionSlug(
                    ps.Id,
                    ps.ProfessionForeignKey,
                    ps.Slug);
                if (psResult.IsFailed)
                    return null;

                return psResult.Value;
            }).ToList();

            if (professionsSlugResult.Count == 0)
                return null;

            return professionsSlugResult;
        }

        public async Task<ProfessionSlug?> GetProfessionSlugAsync(Guid id)
        {
            var professionSlugEntity = await _dbContext.ProfessionsSlug
                .Where(ps => ps.Id == id)
                .SingleOrDefaultAsync();

            if (professionSlugEntity == null)
                return null;

            var professionsSlugResult = ProfessionSlug.CreateProfessionSlug(
                professionSlugEntity.Id,
                professionSlugEntity.ProfessionForeignKey,
                professionSlugEntity.Slug);

            if (professionsSlugResult.IsFailed)
                return null;

            return professionsSlugResult.Value;
        }

        public async Task<ProfessionSlug?> GetProfessionSlugByProfessionIdAsync(Guid professionId)
        {
            var professionSlugEntity = await _dbContext.ProfessionsSlug
                .Where(ps => ps.ProfessionForeignKey == professionId)
                .SingleOrDefaultAsync();

            if (professionSlugEntity == null)
                return null;

            var professionsSlugResult = ProfessionSlug.CreateProfessionSlug(
                professionSlugEntity.Id,
                professionSlugEntity.ProfessionForeignKey,
                professionSlugEntity.Slug);

            if (professionsSlugResult.IsFailed)
                return null;

            return professionsSlugResult.Value;
        }

        public async Task<Guid> CreateProfessionSlugAsync(ProfessionSlug professionSlug)
        {
            ProfessionSlugEntity professionSlugEntity = new()
            {
                Id = professionSlug.Id,
                ProfessionForeignKey = professionSlug.ProfessionId,
                Slug = professionSlug.Slug
            };

            await _dbContext.ProfessionsSlug.AddAsync(professionSlugEntity);
            await _dbContext.SaveChangesAsync();

            return professionSlugEntity.Id;
        }

        public async Task<Guid> UpdateProfessionSlugAsync(Guid id, string slug)
        {
            await _dbContext.ProfessionsSlug
                .ExecuteUpdateAsync(s => s.
                    SetProperty(sp => sp.Slug, sp => slug)
                );

            return id;
        }

        public async Task<Guid> DeleteProfessionSlugAsync(Guid id)
        {
            await _dbContext.ProfessionsSlug
                .Where(sp => sp.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
