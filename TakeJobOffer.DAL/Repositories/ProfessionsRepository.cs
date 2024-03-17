using Microsoft.EntityFrameworkCore;
using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;
using TakeJobOffer.DAL.Entities;

namespace TakeJobOffer.DAL.Repositories
{
    public class ProfessionsRepository : IProfessionsRepository
    {
        private readonly TakeJobOfferDbContext _context;
        public ProfessionsRepository(TakeJobOfferDbContext context)
        {
            _context = context;
        }

        // *Profession data model from Domain
        public async Task<List<Profession>> GetProfessions()
        {
            var professionEntites = await _context.Professions
                .AsNoTracking()
                .ToListAsync();

            // *ProfessionFactory from Domain
            var professions = professionEntites
                .Select(p => Profession.Create(p.Id, p.Name, p.Description).Profession)
                .ToList();

            return professions;
        }

        // *Profession data model from Domain
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

        // *Profession data model from Domain
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
