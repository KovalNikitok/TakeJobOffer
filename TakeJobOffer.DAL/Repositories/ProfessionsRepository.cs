using Microsoft.EntityFrameworkCore;
using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;
using TakeJobOffer.DAL.Entities;
using FluentResults;

namespace TakeJobOffer.DAL.Repositories
{
    public class ProfessionsRepository(TakeJobOfferDbContext dbContext) : IProfessionsRepository
    {
        private readonly TakeJobOfferDbContext _dbContext = dbContext;

        public async Task<List<Profession?>> GetProfessions()
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

        public async Task<Profession?> GetProfessionById(Guid id)
        {
            var professionEntity = await _dbContext.Professions
                .Where(p => p.Id == id)
                .SingleOrDefaultAsync();

            if(professionEntity == null)
                return null;

            var profession = Profession.Create(professionEntity.Id, professionEntity.Name, professionEntity.Description);
            if(profession.IsFailed)
                return null;

            return profession.Value;
        }

        public async Task<Guid> CreateProfession(Profession profession)
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

        public async Task<Guid> UpdateProfession(Guid id, string name, string? description)
        {
            await _dbContext.Professions
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Name, b => name)
                    .SetProperty(b => b.Description, b => description));

            return id;
        }

        public async Task<Guid> DeleteProfession(Guid id)
        {
            await _dbContext.Professions
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
