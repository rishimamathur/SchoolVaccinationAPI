using SchoolVaccinationAPI.Models;
using SchoolVaccinationAPI.Repositories;

namespace SchoolVaccinationAPI.Services
{
    public class StudentVaccinationService : IStudentVaccinationService
    {
        private readonly IStudentVaccinationRepository _repo;
        private readonly IStudentRepository _studentRepo;

        public StudentVaccinationService(IStudentVaccinationRepository repo, IStudentRepository studentRepo)
        {
            _repo = repo;
            _studentRepo = studentRepo;
        }

        public Task<IEnumerable<StudentVaccination>> GetAll() => _repo.GetAll();

        public Task<StudentVaccination?> GetById(int id) => _repo.GetById(id);

        public async Task<string> Add(StudentVaccination sv)
        {
            if (await _repo.Exists(sv.Student_ID, sv.Vaccine_ID))
                return "Student already vaccinated for this vaccine.";

            await _repo.Add(sv);

            // Optionally update student status
            var student = await _studentRepo.GetStudentById(sv.Student_ID);
            if (student != null)
            {
                student.Vaccination_Status = true;
                student.Vaccination_Date = sv.Vaccinated_On;
                await _studentRepo.UpdateStudent(student);
            }

            return "Vaccination recorded successfully.";
        }
    }

}
