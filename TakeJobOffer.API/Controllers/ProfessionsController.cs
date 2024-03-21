using FluentResults;
using Microsoft.AspNetCore.Mvc;
using TakeJobOffer.API.Contracts;
using TakeJobOffer.Domain.Abstractions;
using TakeJobOffer.Domain.Models;

namespace TakeJobOffer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfessionsController : ControllerBase
    {
        private readonly IProfessionsService _professionsService;
        public ProfessionsController(IProfessionsService professionsService)
        {
            _professionsService = professionsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProfessionResponse>>> GetProfessions() 
        {
            var professions = await _professionsService.GetAllProfessions();
            var professionsResponse = professions.Select(p => new ProfessionResponse(p.Id, p.Name, p.Description));

            return Ok(professionsResponse);
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

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateProfession(Guid id, [FromBody] ProfessionRequest professionRequest)
        {
            var professionId = await _professionsService.UpdateProfession(id, professionRequest.Name, professionRequest.Description);

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
