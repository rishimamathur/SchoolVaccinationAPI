using SchoolVaccinationAPI.Models;

namespace SchoolVaccinationAPI.Repositories
{
    public interface IStudentVaccinationRepository
    {
        Task<IEnumerable<StudentVaccination>> GetAll();
        Task<StudentVaccination?> GetById(int id);
        Task Add(StudentVaccination sv);
        Task<bool> Exists(int studentId, int vaccineId);
    }

}
