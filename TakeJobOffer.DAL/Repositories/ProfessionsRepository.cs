using Microsoft.EntityFrameworkCore;
using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;
using TakeJobOffer.DAL.Entities;
using FluentResults;

namespace TakeJobOffer.DAL.Repositories
{
    public class ProfessionsRepository : IProfessionsRepository
    {
        private readonly TakeJobOfferDbContext _context;
        public ProfessionsRepository(TakeJobOfferDbContext context)
        {
            _context = context;
        }

        public async Task<List<Profession?>> GetProfessions()
        {
            var professionEntites = await _context.Professions
                .AsNoTracking()
                .ToListAsync();

            var professions = professionEntites
                .Select((p) =>
                    {
                        var prof = Profession.Create(p.Id, p.Name, p.Description);
                        if (!prof.HasError<Error>())
                            return prof.Value;
                        return null;
                    })
                .ToList();

            return professions;
        }

        public async Task<Guid> CreateProfession(Profession profession)
        {
            var professionEntity = new ProfessionEntity
            {
                Id = profession.Id,
                Name = profession.Name,
                Description = profession.Description,
                Skills = new List<SkillEntity>()
            };

            await _context.Professions.AddAsync(professionEntity);
            await _context.SaveChangesAsync();

            return professionEntity.Id;
        }

        public async Task<Guid> UpdateProfession(Guid id, string name, string description)
        {
            await _context.Professions
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Name, b => name)
                    .SetProperty(b => b.Description, b => description));

            return id;
        }

        public async Task<Guid> DeleteProfession(Guid id)
        {
            await _context.Professions
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
