using Microsoft.EntityFrameworkCore;
using SchoolVaccinationAPI.Data;
using SchoolVaccinationAPI.Models;

namespace SchoolVaccinationAPI.Repositories
{
    public class StudentVaccinationRepository : IStudentVaccinationRepository
    {
        private readonly AppDbContext _context;
        public StudentVaccinationRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<StudentVaccination>> GetAll() =>
            await _context.StudentVaccinations.ToListAsync();

        public async Task<StudentVaccination?> GetById(int id) =>
            await _context.StudentVaccinations.FindAsync(id);

        public async Task Add(StudentVaccination sv)
        {
            _context.StudentVaccinations.Add(sv);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int studentId, int vaccineId) =>
            await _context.StudentVaccinations.AnyAsync(sv =>
                sv.Student_ID == studentId && sv.Vaccine_ID == vaccineId);
    }

}
