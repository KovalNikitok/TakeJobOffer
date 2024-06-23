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
    public class SkillsController(
        ISkillsService skillsService,
        IDistributedCache cache) 
        : ApiController
    {
        private readonly ISkillsService _skillsService = skillsService;
        private readonly IDistributedCache _cache = cache;
        
        [HttpGet]
        public async Task<ActionResult<List<SkillResponse>?>> GetSkills()
        {
            List<SkillResponse>? skillsResponse;

            string cacheKey = "skills";
            string? skillsString = await _cache.GetStringAsync(cacheKey);

            if(!string.IsNullOrEmpty(skillsString))
            {
                skillsResponse = JsonSerializer.Deserialize<List<SkillResponse>>(skillsString);
                return Ok(skillsResponse);
            }

            var skills = await _skillsService.GetAllSkills();

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
        public async Task<ActionResult<SkillResponse>> GetSkill(Guid id)
        {
            var skill = await _skillsService.GetSkillById(id);

            if(skill == null)
            {
                return NotFound();
            }

            var skillResponse = new SkillResponse(skill.Id, skill.Name);

            return Ok(skillResponse);
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<SkillResponse>> GetSkill(string name)
        {
            var skill = await _skillsService.GetSkillByName(name);

            if (skill == null)
            {
                return NotFound();
            }

            var skillResponse = new SkillResponse(skill.Id, skill.Name);

            return Ok(skillResponse);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateSkill([FromBody] SkillRequest skillRequest)
        {
            Result<Skill> skill = Skill.Create(
                Guid.NewGuid(),
                skillRequest.Name);

            if (skill.IsFailed)
                return BadRequest(skill.Errors);

            var skillId = await _skillsService.CreateSkill(skill.Value);

            return Ok(skillId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateSkill(Guid id, [FromBody] SkillRequest skillRequest)
        {
            var skillId = await _skillsService.UpdateSkill(id, skillRequest.Name);

            return Ok(skillId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteSkill(Guid id)
        {
            var deletedSkillId = await _skillsService.DeleteSkill(id);

            return Ok(deletedSkillId);
        }
    }
}
