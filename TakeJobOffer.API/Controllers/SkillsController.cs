using FluentResults;
using Microsoft.AspNetCore.Mvc;
using TakeJobOffer.API.Contracts;
using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.API.Controllers
{
    public class SkillsController : ApiController
    {
        private readonly ISkillsService _skillsService;
        public SkillsController(ISkillsService skillsService)
        {
            _skillsService = skillsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SkillResponse>>> GetSkills()
        {
            var skills = await _skillsService.GetAllSkills();

            if(skills == null || skills.Count == 0)
            {
                return NotFound();
            }

            var skillsResponse = skills.Select(i => new SkillResponse(i!.Id, i!.Name));

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
