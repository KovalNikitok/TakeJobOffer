﻿using Microsoft.EntityFrameworkCore;
using TakeJobOffer.DAL.Entities;
using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.DAL.Repositories
{
    public class ProfessionsSlugRepository(TakeJobOfferDbContext dbContext) : IProfessionsSlugRepository
    {
        private readonly TakeJobOfferDbContext _dbContext = dbContext;

        public async Task<ProfessionSlug?> GetProfessionSlugById(Guid id)
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

        public async Task<ProfessionSlug?> GetProfessionSlugByProfessionId(Guid professionId)
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

        public async Task<Guid> CreateProfessionSlug(ProfessionSlug professionSlug)
        {
            var professionSlugEntity = new ProfessionSlugEntity
            {
                Id = professionSlug.Id,
                ProfessionForeignKey = professionSlug.ProfessionId,
                Slug = professionSlug.Slug
            };

            await _dbContext.ProfessionsSlug.AddAsync(professionSlugEntity);
            await _dbContext.SaveChangesAsync();

            return professionSlugEntity.Id;
        }

        public async Task<Guid> UpdateProfessionSlug(Guid id, string slug)
        {
            await _dbContext.ProfessionsSlug
                .ExecuteUpdateAsync(s => s.
                    SetProperty(sp => sp.Slug, sp => slug)
                );

            return id;
        }

        public async Task<Guid> DeleteProfessionSlug(Guid id)
        {
            await _dbContext.ProfessionsSlug
                .Where(sp => sp.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}