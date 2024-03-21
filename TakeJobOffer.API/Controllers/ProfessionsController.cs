using Microsoft.AspNetCore.Mvc;
using TakeJobOffer.API.Contracts;
using TakeJobOffer.Domain.Abstractions;

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

        [HttpGet("professions")]
        public async Task<ActionResult<List<ProfessionResponse>>> GetProfessions() 
        {
            var professions = await _professionsService.GetAllProfessions();
            var professionsResponse = professions.Select(p => new ProfessionResponse(p.Id, p.Name, p.Description));

            return Ok(professionsResponse);
        }
    }
}
