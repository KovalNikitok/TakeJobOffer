using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using TakeJobOffer.API.Configurations;
using TakeJobOffer.API.Contracts;
using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.API.Controllers
{
    [Route("~/api/professions-skills")]
    public class ProfessionsSkillsController(
        IProfessionsSkillsService professionsSkillsService,
        ISkillsService skillsService,
        IDistributedCache cache) : ApiController
    {
        private readonly IProfessionsSkillsService _professionSkillsService = professionsSkillsService;
        private readonly ISkillsService _skillsService = skillsService;
        private readonly IDistributedCache _cache = cache;

        [HttpGet("{professionId:guid}")]
        public async Task<ActionResult<List<ProfessionSkillResponse>?>> GetProfessionSkillsById(
            Guid professionId)
        {
            List<ProfessionSkillResponse>? professionSkillsResponse;

            string cacheKey = $"professions-skills/{professionId}";
            string? professionsSkillsString = await _cache.GetStringAsync(cacheKey);

            if(!string.IsNullOrEmpty(professionsSkillsString))
            {
                professionSkillsResponse = JsonSerializer.Deserialize<List<ProfessionSkillResponse>?>(
                    professionsSkillsString);
                return Ok(professionSkillsResponse);
            }

            var professionsList = await _professionSkillsService.GetSkillsByProfessionId(professionId);

            if (professionsList == null || professionsList.Count == 0)
            {
                return NotFound();
            }

            professionSkillsResponse = professionsList.Select(p => 
                new ProfessionSkillResponse(p!.SkillId, p.SkillMentionCount))
                .ToList();

            professionsSkillsString = JsonSerializer.Serialize(professionSkillsResponse);
            await _cache.SetStringAsync(cacheKey, professionsSkillsString, CacheOptions.SlidingFiveMinuteOption);

            return Ok(professionSkillsResponse);
        }

        [HttpGet("{professionId:guid}/with-name")]
        public async Task<ActionResult<List<ProfessionSkillWithNameResponse?>?>> GetProfessionSkillsWithNameById(
            Guid professionId,
            [FromQuery] bool isOrdered)
        {
            List<ProfessionSkillWithNameResponse?>? professionsSkillsResponse;

            string cacheKey = $"professions-skills/{professionId}/with-name";
            string? professionsSkillsString = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(professionsSkillsString))
            {
                professionsSkillsResponse = JsonSerializer.Deserialize<List<ProfessionSkillWithNameResponse?>?>(
                    professionsSkillsString);
            }
            else
            {
                var professionSkillsList = await _professionSkillsService.GetSkillsByProfessionId(professionId);

                if (professionSkillsList == null || professionSkillsList.Count == 0)
                {
                    return NotFound();
                }

                IEnumerable<Guid> skillsIds = professionSkillsList.Select(ps => ps!.SkillId);

                var skills = await _skillsService.GetSkillsByIds(skillsIds);

                if (skills == null || skills.Count == 0)
                {
                    return NotFound();
                }

                professionsSkillsResponse = professionSkillsList.Select(p =>
                {
                    var skill = skills.Where(s =>
                        s!.Id == p!.SkillId).FirstOrDefault();
                    if (skill == null)
                        return null;

                    return new ProfessionSkillWithNameResponse(p!.SkillId, skill.Name, p.SkillMentionCount);
                }).ToList();

                professionsSkillsString = JsonSerializer.Serialize(professionsSkillsResponse);
                await _cache.SetStringAsync(cacheKey, professionsSkillsString, CacheOptions.SlidingFiveMinuteOption);
            }

            if (isOrdered)
                return Ok(professionsSkillsResponse?.OrderByDescending(p => p?.MentionCount));

            return Ok(professionsSkillsResponse);
        }

        [HttpPost("{professionId:guid}")]
        public async Task<ActionResult<Guid?>> PostProfessionSkill(Guid professionId, 
            [FromBody] ProfessionSkillWithSkillMentionRequest professionSkillResponse)
        {
            Result<ProfessionSkill> professionSkillResult = ProfessionSkill.CreateProfessionSkill(
                professionId,
                professionSkillResponse.SkillId,
                professionSkillResponse.SkillMentionCount
            );

            if (professionSkillResult.IsFailed)
                return BadRequest(professionSkillResult.Errors);

            var professionsId = await _professionSkillsService.CreateSkillForProfessionById(
                professionSkillResult.Value);

            return CreatedAtAction("PostProfessionSkill", professionsId);
        }

        [HttpPut("{professionId:guid}")]
        public async Task<ActionResult<Guid>> UpdateProfessionSkill(Guid professionId, 
            [FromBody] ProfessionSkillWithSkillMentionRequest professionSkillResponse)
        {
            Result<ProfessionSkill> professionSkillResult = ProfessionSkill.CreateProfessionSkill(
                professionId,
                professionSkillResponse.SkillId,
                professionSkillResponse.SkillMentionCount
            );

            if (professionSkillResult.IsFailed)
                return BadRequest(professionSkillResult.Errors);

            var professionSkill = professionSkillResult.Value;

            Guid? updatedProfessionsId = await _professionSkillsService.UpdateSkillMentionCountForProfessionById(
                professionSkill.ProfessionId,
                professionSkill.SkillId,
                professionSkill.SkillMentionCount);

            if (updatedProfessionsId == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{professionId:guid}/{psId:guid}")]
        public async Task<ActionResult<Guid>> DeleteProfessionSkill(
            Guid professionId,
            Guid psId)
        {
            var profId = await _professionSkillsService.DeleteSkillForProfessionById(
                professionId,
                psId);

            return NoContent();
        }
    }
}
