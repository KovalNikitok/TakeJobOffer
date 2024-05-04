using Microsoft.AspNetCore.Mvc;
using TakeJobOffer.API.Contracts;
using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.API.Controllers
{
    public class ProfessionsSlugController(IProfessionsSlugService professionsSlugService) : ApiController
    {
        private readonly IProfessionsSlugService _professionsSlugService = professionsSlugService;

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProfessionSlugResponse>> GetProfessionSlugById(Guid id)
        {
            var professionSlug = await _professionsSlugService.GetProfessionSlugById(id);

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
        public async Task<ActionResult<Guid>> CreateProfessionSlug(Guid professionId,
            [FromBody] ProfessionSlugRequest professionSlugRequest)
        {
            var professionResult = ProfessionSlug.CreateProfessionSlug(
                Guid.NewGuid(),
                professionId,
                professionSlugRequest.Slug);

            if (professionResult.IsFailed)
                return BadRequest(professionResult.Errors);

            var id = await _professionsSlugService.CreateProfessionSlug(professionResult.Value);

            return Ok(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateProfessionSlug(Guid id,
            [FromBody] ProfessionSlugRequest professionSlugRequest)
        {
            var responseId = await _professionsSlugService.UpdateProfessionSlug(id, professionSlugRequest.Slug);

            return Ok(responseId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteProfessionSlug(Guid id)
        {
            var responseId = await _professionsSlugService.DeleteProfessionSlug(id);

            return Ok(responseId);
        }
    }
}
