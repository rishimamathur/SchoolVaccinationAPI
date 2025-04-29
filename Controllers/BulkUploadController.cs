using Microsoft.AspNetCore.Mvc;
using SchoolVaccinationAPI.Models;
using SchoolVaccinationAPI.Services;

namespace SchoolVaccinationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BulkUploadController : ControllerBase
    {
        private readonly IStudentService _service;

        public BulkUploadController(IStudentService service) => _service = service;

        [HttpPost("students/csv")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadStudents([FromForm] UploadStudentCsvRequest request)
        {
            var file = request.File;
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var students = new List<Student>();
            using var stream = new StreamReader(file.OpenReadStream());
            while (!stream.EndOfStream)
            {
                var line = await stream.ReadLineAsync();
                var columns = line?.Split(',');
                if (columns != null && columns.Length >= 3)
                {
                    students.Add(new Student
                    {
                        First_Name = columns[0],
                        Last_Name = columns[1],
                        Class = columns[2],
                        Vaccination_Status = !string.IsNullOrEmpty(columns[3]) ? Convert.ToBoolean(columns[3]) : false,
                        Vaccination_Date = !string.IsNullOrEmpty(columns[4]) ? DateTime.Parse(columns[4]) : null,
                        Vaccine_ID = !string.IsNullOrEmpty(columns[5]) ? Convert.ToInt32(columns[5]) : null
                    });
                }
            }

            foreach (var student in students)
            {
                await _service.AddStudent(student);
            }

            return Ok($"{students.Count} students uploaded successfully.");
        }
    }
}
