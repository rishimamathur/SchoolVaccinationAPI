using Microsoft.AspNetCore.Mvc;
using SchoolVaccinationAPI.Models;
using SchoolVaccinationAPI.Services;

namespace SchoolVaccinationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccinationController : ControllerBase
    {
        private readonly IVaccinationService _service;
        public VaccinationController(IVaccinationService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllVaccinations());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vaccination = await _service.GetVaccinationById(id);
            return vaccination is not null ? Ok(vaccination) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vaccination vaccination)
        {
            await _service.AddVaccination(vaccination);
            return CreatedAtAction(nameof(Get), new { id = vaccination.Vaccine_ID }, vaccination);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Vaccination vaccination)
        {
            if (id != vaccination.Vaccine_ID) return BadRequest();
            await _service.UpdateVaccination(vaccination);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteVaccination(id);
            return NoContent();
        }
    }
}
