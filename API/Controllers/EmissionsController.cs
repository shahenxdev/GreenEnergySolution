using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmissionsController : ControllerBase
    {
        private readonly IEmissionService _emissionService;

        public EmissionsController(IEmissionService emissionService)
        {
            _emissionService = emissionService;
        }

        [HttpPost]
        public IActionResult AddEmission([FromBody] Emission emission)
        {
            if (string.IsNullOrEmpty(emission.Type) || emission.Amount <= 0)
                return BadRequest("Invalid emission data");

            _emissionService.AddEmission(emission);
            return Ok(new { message = "Emission added" });
        }

        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            var summary = _emissionService.GetSummary();
            return Ok(summary);
        }
    }
}
