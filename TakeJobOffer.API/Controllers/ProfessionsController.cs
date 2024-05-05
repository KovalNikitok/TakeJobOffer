using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TakeJobOffer.API.Contracts;
using TakeJobOffer.Application.Services;
using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.API.Controllers
{
    public class ProfessionsController(IProfessionsService professionsService, IProfessionsSlugService professionsSlugService) : ApiController
    {
        private readonly IProfessionsService _professionsService = professionsService;
        private readonly IProfessionsSlugService _professionsSlugService = professionsSlugService;

        [HttpGet]
        public async Task<ActionResult<List<ProfessionResponse>?>> GetProfessions()
        {
            var professions = await _professionsService.GetAllProfessions();

            if (professions == null || professions.Count == 0)
            {
                return NotFound("Professions not found");
            }

            var professionsResponse = professions.Select(p => new ProfessionResponse(p!.Id, p!.Name, p.Description));

            return Ok(professionsResponse);
        }

        [HttpGet("with-slug")]
        public async Task<ActionResult<List<ProfessionWithSlugResponse>?>> GetProfessionsWithSlug()
        {
            var professions = await _professionsService.GetAllProfessions();

            if (professions == null || professions.Count == 0)
            {
                return NotFound("Professions not found");
            }

            IEnumerable<Guid> ids = professions.Select(p => p!.Id);
            var professionSlugs = await _professionsSlugService.GetProfessionSlugsByProfessionsIds(ids);

            if(professionSlugs is null || professionSlugs.Count == 0)
                return NotFound("Slugs for professions was not found");

            var professionsWithSlugList = 
                (from profession in professions
                join professionSlug in professionSlugs
                on profession.Id equals professionSlug.ProfessionId
                select new ProfessionWithSlugResponse(
                    profession.Id,
                    profession.Name,
                    profession.Description,
                    professionSlug.Slug))
                .ToList();

            return Ok(professionsWithSlugList);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProfessionResponse>?> GetProfessionById(Guid id)
        {
            var profession = await _professionsService.GetProfessionById(id);

            if (profession == null)
            {
                return NotFound("Profession by that Id not found");
            }

            var professionResponse = new ProfessionResponse(profession!.Id, profession!.Name, profession.Description);

            return Ok(professionResponse);
        }

        [HttpGet("{id:guid}/with-slug")]
        public async Task<ActionResult<ProfessionResponse>?> GetProfessionByIdWithSlug(Guid id)
        {
            var profession = await _professionsService.GetProfessionById(id);

            if (profession == null)
                return NotFound("Profession by that Id was not found");

            var slug = await _professionsSlugService.GetProfessionSlugByProfessionId(id);

            if (slug == null)
                return NotFound("Slug for Profession by that Id was not found");

            var professionResponse = new ProfessionWithSlugResponse(
                    profession!.Id,
                    profession!.Name,
                    profession.Description,
                    slug.Slug);

            return Ok(professionResponse);
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<ProfessionResponse>?> GetProfessionBySlug(string slug)
        {
            var profession = await _professionsService.GetProfessionBySlug(slug);

            if (profession == null)
            {
                return NotFound("Profession by that slug string was not found");
            }

            var professionResponse = new ProfessionResponse(profession!.Id, profession!.Name, profession.Description);

            return Ok(professionResponse);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> PostProfession([FromBody] ProfessionRequest professionRequest)
        {
            Result<Profession> professionResult = Profession.Create(
                Guid.NewGuid(),
                professionRequest.Name,
                professionRequest.Description);

            if (professionResult.IsFailed)
            {
                return BadRequest(professionResult.Errors);
            }

            var professionId = await _professionsService.CreateProfession(professionResult.Value);

            return Ok(professionId);
        }

        [HttpPost("with-slug")]
        public async Task<ActionResult<Guid>> PostProfessionWithSlug([FromBody] ProfessionWithSlugRequest professionRequest)
        {
            Result<Profession> professionResult = Profession.Create(
                Guid.NewGuid(),
                professionRequest.Name,
                professionRequest.Description);

            if (professionResult.IsFailed)
                return BadRequest(professionResult.Errors);

            var profession = professionResult.Value;
            Result<ProfessionSlug> professionSlugResult = ProfessionSlug.CreateProfessionSlug(
                Guid.NewGuid(),
                profession.Id,
                professionRequest.Slug);

            if (professionSlugResult.IsFailed)
                return BadRequest(professionSlugResult.Errors);
            
            var professionSlug = professionSlugResult.Value;

            var professionId = await _professionsService.CreateProfessionWithSlug(profession, professionSlug);

            return Ok(professionId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateProfession(Guid id, [FromBody] ProfessionRequest professionRequest)
        {
            var professionId = await _professionsService.UpdateProfession(id, professionRequest.Name, professionRequest.Description);

            return Ok(professionId);
        }

        [HttpPut("{id:guid}/with-slug")]
        public async Task<ActionResult<Guid>> UpdateProfessionWithSlug(Guid id, [FromBody] ProfessionWithSlugRequest professionRequest)
        {
            var professionId = await _professionsService.UpdateProfession(id, professionRequest.Name, professionRequest.Description);

            var professionSlug = await _professionsSlugService.GetProfessionSlugByProfessionId(professionId);
            if (professionSlug is not null && professionSlug.Slug != professionRequest.Slug)
                await _professionsSlugService.UpdateProfessionSlug(professionSlug.Id, professionRequest.Slug);

            return Ok(professionId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteProfession(Guid id)
        {
            var professionId = await _professionsService.DeleteProfession(id);

            return Ok(professionId);
        }

    }
}
