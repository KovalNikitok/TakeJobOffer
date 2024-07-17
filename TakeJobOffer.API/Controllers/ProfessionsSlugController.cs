using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using TakeJobOffer.API.Configurations;
using TakeJobOffer.API.Contracts;
using TakeJobOffer.Domain.Abstractions.Services;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.API.Controllers
{
    [Route("~/api/professions-slug")]
    public class ProfessionsSlugController(
        IProfessionsSlugService professionsSlugService,
        IDistributedCache cache) 
        : ApiController
    {
        private readonly IProfessionsSlugService _professionsSlugService = professionsSlugService;
        private readonly IDistributedCache _cache = cache;
        
        [HttpGet]
        public async Task<ActionResult<List<ProfessionSlugResponse?>?>> GetProfessionsSlugAsync()
        {
            List<ProfessionSlugResponse?>? response;

            string cacheKey = "professions-slug";
            string? professionSlugsString = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(professionSlugsString))
            {
                response = JsonSerializer.Deserialize<List<ProfessionSlugResponse?>?>(cacheKey);
                return Ok(response);
            }

            var professionSlug = await _professionsSlugService.GetProfessionSlugsAsync();

            if (professionSlug == null || professionSlug.Count == 0)
                return NotFound("Professions slug was not found");

            response = professionSlug.Select(ps =>
            {
                if (ps is null)
                    return null;

                return new ProfessionSlugResponse(
                    ps.Id,
                    ps.ProfessionId,
                    ps.Slug);
            }).ToList();

            professionSlugsString = JsonSerializer.Serialize(response);
            await _cache.SetStringAsync(cacheKey, professionSlugsString, CacheOptions.SlidingFiveMinuteOption);

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProfessionSlugResponse>> GetProfessionSlugAsync(Guid id)
        {
            var professionSlug = await _professionsSlugService.GetProfessionSlugAsync(id);

            if (professionSlug == null)
                return NotFound("Profession slug was not found");

            ProfessionSlugResponse response = new(
                professionSlug.Id,
                professionSlug.ProfessionId,
                professionSlug.Slug);

            return Ok(response);
        }

        [HttpGet("p/{professionId:guid}")]
        public async Task<ActionResult<ProfessionSlugResponse>> GetProfessionSlugByProfessionId(Guid professionId)
        {
            var professionSlug = await _professionsSlugService.GetProfessionSlugByProfessionId(professionId);

            if (professionSlug == null)
                return NotFound("Profession slug was not found by that profession id");

            ProfessionSlugResponse response = new(
                professionSlug.Id,
                professionSlug.ProfessionId,
                professionSlug.Slug);

            return Ok(response);
        }

        [HttpPost("{professionId:guid}")]
        public async Task<ActionResult<Guid>> CreateProfessionSlugAsync(Guid professionId,
            [FromBody] ProfessionSlugRequest professionSlugRequest)
        {
            var professionSlugResult = ProfessionSlug.CreateProfessionSlug(
                Guid.NewGuid(),
                professionId,
                professionSlugRequest.Slug);

            if (professionSlugResult.IsFailed)
                return BadRequest(professionSlugResult.Errors);

            var id = await _professionsSlugService.CreateProfessionSlugAsync(professionSlugResult.Value);

            return CreatedAtAction(nameof(CreateProfessionSlugAsync), id);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateProfessionSlugAsync(Guid id,
            [FromBody] ProfessionSlugRequest professionSlugRequest)
        {
            var responseId = await _professionsSlugService.UpdateProfessionSlugAsync(id, professionSlugRequest.Slug);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteProfessionSlugAsync(Guid id)
        {
            var responseId = await _professionsSlugService.DeleteProfessionSlugAsync(id);

            return NoContent();
        }
    }
}
