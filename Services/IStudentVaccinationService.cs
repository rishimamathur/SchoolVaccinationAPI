using SchoolVaccinationAPI.Models;

namespace SchoolVaccinationAPI.Services
{
    public interface IStudentVaccinationService
    {
        Task<IEnumerable<StudentVaccination>> GetAll();
        Task<StudentVaccination?> GetById(int id);
        Task<string> Add(StudentVaccination sv);
    }
}
