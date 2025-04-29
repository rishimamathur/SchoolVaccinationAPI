using Microsoft.AspNetCore.Mvc;
using SchoolVaccinationAPI.Models;
using SchoolVaccinationAPI.Services;

namespace SchoolVaccinationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        public StudentController(IStudentService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllStudents());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _service.GetStudentById(id);
            return student is not null ? Ok(student) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            await _service.AddStudent(student);
            return CreatedAtAction(nameof(Get), new { id = student.Student_ID }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Student student)
        {
            if (id != student.Student_ID) return BadRequest();
            await _service.UpdateStudent(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteStudent(id);
            return NoContent();
        }
    }
}
