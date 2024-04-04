using FluentResults;
using Microsoft.AspNetCore.Mvc;
using TakeJobOffer.API.Contracts;
using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.API.Controllers
{
    [Route("~/api/professions-skills")]
    public class ProfessionsSkillsController(IProfessionsSkillsService professionsSkillsService) : ApiController
    {
        private readonly IProfessionsSkillsService _professionsService = professionsSkillsService;

        [HttpGet("{professionId:guid}")]
        public async Task<ActionResult<List<ProfessionSkillResponse>?>> GetProfessionSkillsById(Guid professionId)
        {
            var professionsList = await _professionsService.GetSkillsByProfessionId(professionId);

            if (professionsList == null || professionsList.Count == 0)
            {
                return NotFound();
            }

            var professionSkillsResponse = professionsList.Select(p => 
                new ProfessionSkillResponse(p!.SkillId, p.SkillMentionCount))
                .ToList();

            return Ok(professionSkillsResponse);
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

            var professionsId = await _professionsService.CreateSkillForProfessionById(professionSkillResult.Value);

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

            Guid? updatedProfessionsId = await _professionsService.UpdateSkillMentionCountForProfessionById(
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
            var profId = await _professionsService.DeleteSkillForProfessionById(professionId, professionSkillResult.SkillId);

            return Ok(profId);
        }
    }
}
