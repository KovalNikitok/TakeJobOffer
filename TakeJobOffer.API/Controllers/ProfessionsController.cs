﻿using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using TakeJobOffer.API.Contracts;
using TakeJobOffer.Domain.Models;
using TakeJobOffer.API.Configurations;
using TakeJobOffer.Domain.Abstractions.Services;

namespace TakeJobOffer.API.Controllers
{
    public class ProfessionsController(
        IProfessionsService professionsService,
        IProfessionsSlugService professionsSlugService,
        IDistributedCache cache)
        : ApiController
    {
        private readonly IProfessionsService _professionsService = professionsService;
        private readonly IProfessionsSlugService _professionsSlugService = professionsSlugService;
        private readonly IDistributedCache _cache = cache;

        
        [HttpGet]
        public async Task<ActionResult<List<ProfessionResponse>?>> GetProfessionsAsync()
        {
            List<ProfessionResponse>? professionsResponse;
            string cacheKey = "professions";

            string? professionsString = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(professionsString))
            {
                professionsResponse = JsonSerializer.Deserialize<List<ProfessionResponse>>(professionsString);
                return Ok(professionsResponse);
            }

            var professions = await _professionsService.GetProfessionsAsync();

            if (professions == null || professions.Count == 0)
            {
                return NotFound("Professions not found");
            }

            professionsResponse = professions.Select(p => new ProfessionResponse(p!.Id, p!.Name, p.Description)).ToList();

            professionsString = JsonSerializer.Serialize(professionsResponse);
            await _cache.SetStringAsync(cacheKey, professionsString, CacheOptions.SlidingFiveMinuteOption);

            return Ok(professionsResponse);
        }
        
        [HttpGet("with-slug")]
        public async Task<ActionResult<List<ProfessionWithSlugResponse>?>> GetProfessionsWithSlugAsync()
        {
            List<ProfessionWithSlugResponse>? professionResponse;

            string cacheKey = "professions/with-slug";
            string? professionsString = await _cache.GetStringAsync(cacheKey);

            if(!string.IsNullOrEmpty(professionsString))
            {
                professionResponse = JsonSerializer.Deserialize<List<ProfessionWithSlugResponse>>(professionsString);
                return Ok(professionResponse);
            }

            var professions = await _professionsService.GetProfessionsAsync();

            if (professions == null || professions.Count == 0)
            {
                return NotFound("Professions not found");
            }

            IEnumerable<Guid> ids = professions.Select(p => p!.Id);
            var professionSlugs = await _professionsSlugService.GetProfessionSlugsAsync(ids);

            if (professionSlugs is null || professionSlugs.Count == 0)
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

            professionsString = JsonSerializer.Serialize(professionsWithSlugList);
            await _cache.SetStringAsync(cacheKey, professionsString, CacheOptions.SlidingFiveMinuteOption);

            return Ok(professionsWithSlugList);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProfessionResponse>?> GetProfessionAsync(Guid id)
        {
            var profession = await _professionsService.GetProfessionAsync(id);

            if (profession == null)
            {
                return NotFound("Profession by that Id not found");
            }

            var professionResponse = new ProfessionResponse(profession!.Id, profession!.Name, profession.Description);

            return Ok(professionResponse);
        }
        
        [HttpGet("{id:guid}/with-slug")]
        public async Task<ActionResult<ProfessionResponse>?> GetProfessionWithSlugAsync(Guid id)
        {
            var profession = await _professionsService.GetProfessionAsync(id);

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
        public async Task<ActionResult<ProfessionResponse>?> GetProfessionAsync(string slug)
        {
            ProfessionResponse? professionResponse;

            string cacheKey = $"professions/{slug}";
            string? professionString = await _cache.GetStringAsync(cacheKey);

            if(!string.IsNullOrEmpty(professionString))
            {
                professionResponse = JsonSerializer.Deserialize<ProfessionResponse?>(professionString);
                return Ok(professionResponse);
            }

            var profession = await _professionsService.GetProfessionAsync(slug);

            if (profession == null)
            {
                return NotFound("Profession by that slug string was not found");
            }

            professionResponse = new ProfessionResponse(profession!.Id, profession!.Name, profession.Description);

            professionString = JsonSerializer.Serialize(professionResponse);
            await _cache.SetStringAsync(cacheKey, professionString, CacheOptions.SlidingFiveMinuteOption);

            return Ok(professionResponse);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> PostProfessionAsync([FromBody] ProfessionRequest professionRequest)
        {
            Result<Profession> professionResult = Profession.Create(
                Guid.NewGuid(),
                professionRequest.Name,
                professionRequest.Description);

            if (professionResult.IsFailed)
            {
                return BadRequest(professionResult.Errors);
            }

            var professionId = await _professionsService.CreateProfessionAsync(professionResult.Value);

            return CreatedAtAction(nameof(PostProfessionAsync), professionId);
        }

        [HttpPost("with-slug")]
        public async Task<ActionResult<Guid>> PostProfessionWithSlugAsync([FromBody] ProfessionWithSlugRequest professionRequest)
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

            var professionId = await _professionsService.CreateProfessionWithSlugAsync(profession, professionSlug);

            if (professionId == Guid.Empty)
                return BadRequest("Profession already exist");

            return CreatedAtAction(nameof(PostProfessionWithSlugAsync), professionId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateProfessionAsync(Guid id, [FromBody] ProfessionRequest professionRequest)
        {
            var professionId = await _professionsService.UpdateProfessionAsync(id, professionRequest.Name, professionRequest.Description);

            return NoContent();
        }

        [HttpPut("{id:guid}/with-slug")]
        public async Task<ActionResult<Guid>> UpdateProfessionWithSlug(Guid id, [FromBody] ProfessionWithSlugRequest professionRequest)
        {
            var professionId = await _professionsService.UpdateProfessionAsync(id, professionRequest.Name, professionRequest.Description);

            var professionSlug = await _professionsSlugService.GetProfessionSlugByProfessionId(professionId);
            if (professionSlug is not null && professionSlug.Slug != professionRequest.Slug)
                await _professionsSlugService.UpdateProfessionSlugAsync(professionSlug.Id, professionRequest.Slug);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteProfessionAsync(Guid id)
        {
            var professionId = await _professionsService.DeleteProfessionAsync(id);

            return NoContent();
        }
    }
}
