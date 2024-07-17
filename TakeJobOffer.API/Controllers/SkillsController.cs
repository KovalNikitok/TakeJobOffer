using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using TakeJobOffer.API.Configurations;
using TakeJobOffer.API.Contracts;
using TakeJobOffer.Domain.Abstractions.Services;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.API.Controllers
{
    public class SkillsController(
        ISkillsService skillsService,
        IDistributedCache cache) 
        : ApiController
    {
        private readonly ISkillsService _skillsService = skillsService;
        private readonly IDistributedCache _cache = cache;
        
        [HttpGet]
        public async Task<ActionResult<List<SkillResponse>?>> GetSkillsAsync()
        {
            List<SkillResponse>? skillsResponse;

            string cacheKey = "skills";
            string? skillsString = await _cache.GetStringAsync(cacheKey);

            if(!string.IsNullOrEmpty(skillsString))
            {
                skillsResponse = JsonSerializer.Deserialize<List<SkillResponse>>(skillsString);
                return Ok(skillsResponse);
            }

            var skills = await _skillsService.GetSkillsAsync();

            if(skills == null || skills.Count == 0)
            {
                return NotFound();
            }

            skillsResponse = skills.Select(i => new SkillResponse(i!.Id, i!.Name)).ToList();

            skillsString = JsonSerializer.Serialize(skillsResponse);
            await _cache.SetStringAsync(cacheKey, skillsString, CacheOptions.SlidingFiveMinuteOption);

            return Ok(skillsResponse);
        }

        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SkillResponse>> GetSkillAsync(Guid id)
        {
            var skill = await _skillsService.GetSkillAsync(id);

            if(skill == null)
            {
                return NotFound();
            }

            var skillResponse = new SkillResponse(skill.Id, skill.Name);

            return Ok(skillResponse);
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<SkillResponse>> GetSkillAsync(string name)
        {
            var skill = await _skillsService.GetSkillAsync(name);

            if (skill == null)
            {
                return NotFound();
            }

            var skillResponse = new SkillResponse(skill.Id, skill.Name);

            return Ok(skillResponse);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateSkillAsync([FromBody] SkillRequest skillRequest)
        {
            Result<Skill> skill = Skill.Create(
                Guid.NewGuid(),
                skillRequest.Name);

            if (skill.IsFailed)
                return BadRequest(skill.Errors);

            var skillId = await _skillsService.CreateSkillAsync(skill.Value);

            return CreatedAtAction(nameof(CreateSkillAsync), skillId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateSkillAsync(Guid id, [FromBody] SkillRequest skillRequest)
        {
            var skillId = await _skillsService.UpdateSkillAsync(id, skillRequest.Name);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteSkillAsync(Guid id)
        {
            var deletedSkillId = await _skillsService.DeleteSkillAsync(id);

            return NoContent();
        }
    }
}
