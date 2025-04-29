using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolVaccinationAPI.Models;
using SchoolVaccinationAPI.Services;

namespace SchoolVaccinationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentVaccinationController : ControllerBase
    {
        private readonly IStudentVaccinationService _service;

        public StudentVaccinationController(IStudentVaccinationService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var record = await _service.GetById(id);
            return record is not null ? Ok(record) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentVaccination sv)
        {
            var result = await _service.Add(sv);
            if (result.Contains("already"))
                return Conflict(result);

            return Ok(result);
        }
    }

}
