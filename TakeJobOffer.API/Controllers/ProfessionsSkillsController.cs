using FluentResults;
using Microsoft.AspNetCore.Mvc;
using TakeJobOffer.API.Contracts;
using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.API.Controllers
{
    [Route("~/api/professions-skills")]
    public class ProfessionsSkillsController(IProfessionsSkillsService professionsSkillsService, ISkillsService skillsService) : ApiController
    {
        private readonly IProfessionsSkillsService _professionSkillsService = professionsSkillsService;
        private readonly ISkillsService _skillsService = skillsService;

        [HttpGet("{professionId:guid}")]
        public async Task<ActionResult<List<ProfessionSkillResponse>?>> GetProfessionSkillsById(Guid professionId)
        {
            var professionsList = await _professionSkillsService.GetSkillsByProfessionId(professionId);

            if (professionsList == null || professionsList.Count == 0)
            {
                return NotFound();
            }

            var professionSkillsResponse = professionsList.Select(p => 
                new ProfessionSkillResponse(p!.SkillId, p.SkillMentionCount))
                .ToList();

            return Ok(professionSkillsResponse);
        }

        [HttpGet("{professionId:guid}/with-name")]
        public async Task<ActionResult<List<ProfessionSkillWithNameResponse?>?>> GetProfessionSkillsWithNameById(Guid professionId,
            [FromQuery] bool isOrdered)
        {
            var professionSkillsList = await _professionSkillsService.GetSkillsByProfessionId(professionId);

            if (professionSkillsList == null || professionSkillsList.Count == 0)
            {
                return NotFound();
            }

            IEnumerable<Guid> skillsIds = professionSkillsList.Select(ps => ps!.SkillId);

            var skills = await _skillsService.GetSkillsByIds(skillsIds);

            if(skills == null || skills.Count == 0) 
            {  
                return NotFound(); 
            }

            var professionsSkillsResponse = professionSkillsList.Select(p =>
            {
                var skill = skills.Where(s =>
                    s!.Id == p!.SkillId).FirstOrDefault();
                if (skill == null)
                    return null;

                return new ProfessionSkillWithNameResponse(p!.SkillId, skill.Name, p.SkillMentionCount);
            });

            if (isOrdered)
                return Ok(professionsSkillsResponse.OrderByDescending(p => p?.MentionCount).ToList());

            return Ok(professionsSkillsResponse.ToList());
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

            var professionsId = await _professionSkillsService.CreateSkillForProfessionById(professionSkillResult.Value);

            return Ok(professionsId);
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

            return Ok(updatedProfessionsId);
        }

        [HttpDelete("{professionId:guid}")]
        public async Task<ActionResult<Guid>> DeleteProfessionSkill(Guid professionId, 
            [FromBody] ProfessionSkillRequest professionSkillResult)
        {
            var profId = await _professionSkillsService.DeleteSkillForProfessionById(professionId, professionSkillResult.SkillId);

            return Ok(profId);
        }
    }
}
