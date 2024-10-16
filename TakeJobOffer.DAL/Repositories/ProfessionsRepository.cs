﻿using Microsoft.EntityFrameworkCore;
using TakeJobOffer.Domain.Models;
using TakeJobOffer.DAL.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using TakeJobOffer.Domain.Abstractions.Repositories;

namespace TakeJobOffer.DAL.Repositories
{
    public class ProfessionsRepository(TakeJobOfferDbContext dbContext) : IProfessionsRepository
    {
        private readonly TakeJobOfferDbContext _dbContext = dbContext;

        public async Task<List<Profession?>> GetProfessionsAsync()
        {
            var professionEntites = await _dbContext.Professions
                .AsNoTracking()
                .ToListAsync();

            var professions = professionEntites
                .Select((p) =>
                    {
                        var profession = Profession.Create(p.Id, p.Name, p.Description);
                        if (profession.IsSuccess)
                            return profession.Value;
                        return null;
                    })
                .ToList();

            return professions;
        }

        public async Task<Profession?> GetProfessionAsync(Guid id)
        {
            var professionEntity = await _dbContext.Professions
                .Where(p => p.Id == id)
                .SingleOrDefaultAsync();

            if (professionEntity == null)
                return null;

            var profession = Profession.Create(professionEntity.Id, professionEntity.Name, professionEntity.Description);
            if (profession.IsFailed)
                return null;

            return profession.Value;
        }

        public async Task<Profession?> GetProfessionAsync(string slug)
        {
            var professionEntity = await _dbContext.ProfessionsSlug
                .Where(ps => ps.Slug == slug)
                .Select(ps => ps.Profession)
                .SingleOrDefaultAsync();

            if (professionEntity == null)
                return null;

            var profession = Profession.Create(professionEntity.Id, professionEntity.Name, professionEntity.Description);
            if (profession.IsFailed)
                return null;

            return profession.Value;
        }

        public async Task<Guid> CreateProfessionAsync(Profession profession)
        {
            var professionEntity = new ProfessionEntity
            {
                Id = profession.Id,
                Name = profession.Name,
                Description = profession.Description,
                Skills = []
            };

            await _dbContext.Professions.AddAsync(professionEntity);
            await _dbContext.SaveChangesAsync();

            return professionEntity.Id;
        }

        public async Task<Guid> CreateProfessionWithSlugAsync(Profession profession, ProfessionSlug professionSlug)
        {

            await using IDbContextTransaction transactionCheck =
                await _dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Snapshot);
            try
            {
                var professionExisted = await _dbContext.Professions.Where(p => p.Name == profession.Name)
                    .FirstOrDefaultAsync();

                var professionSlugExisted = await _dbContext.ProfessionsSlug.Where(p => p.Slug == professionSlug.Slug)
                    .FirstOrDefaultAsync();

                if (professionExisted is not null || professionSlugExisted is not null)
                    return Guid.Empty;

                await transactionCheck.CommitAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                await transactionCheck.RollbackAsync().ConfigureAwait(false);
                throw;
            }

            ProfessionSlugEntity professionSlugEntity = new()
            {
                Id = professionSlug.Id,
                ProfessionForeignKey = profession.Id,
                Slug = professionSlug.Slug,
            };

            ProfessionEntity professionEntity = new()
            {
                Id = profession.Id,
                Name = profession.Name,
                Description = profession.Description,
                Skills = [],
                ProfessionSlug = professionSlugEntity
            };

            professionSlugEntity.Profession = professionEntity;

            await using IDbContextTransaction transaction =
                await _dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Snapshot);
            try
            {
                await _dbContext.Professions.AddAsync(professionEntity);
                await _dbContext.ProfessionsSlug.AddAsync(professionSlugEntity);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync().ConfigureAwait(false);

                return professionEntity.Id;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task<Guid> UpdateProfessionAsync(Guid id, string name, string? description)
        {
            await _dbContext.Professions
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Name, b => name)
                    .SetProperty(b => b.Description, b => description));

            return id;
        }

        public async Task<Guid> DeleteProfessionAsync(Guid id)
        {
            await _dbContext.Professions
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
