using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolVaccinationAPI.Data;
using SchoolVaccinationAPI.Models;

namespace SchoolVaccinationAPI.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Student>> GetAllStudents() => await _context.Students.Include(s => s.StudentVaccinations).ToListAsync();

        public async Task<Student?> GetStudentById(int id) => await _context.Students.FindAsync(id);

        public async Task AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            if (student.Vaccine_ID.HasValue && student.Vaccination_Date.HasValue)
            {
                var mapping = new StudentVaccination
                {
                    Student_ID = student.Student_ID,
                    Vaccine_ID = student.Vaccine_ID.Value,
                    Vaccinated_On = student.Vaccination_Date.Value
                };

                _context.StudentVaccinations.Add(mapping);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            var existingStudent = await _context.Students.FindAsync(student.Student_ID);
            if (existingStudent == null) return;

            existingStudent.Vaccination_Date = student.Vaccination_Date;
            existingStudent.Vaccination_Status = student.Vaccine_ID.HasValue;

            _context.Students.Update(existingStudent);

            var existingMap = await _context.StudentVaccinations
                .FirstOrDefaultAsync(m => m.Student_ID == student.Student_ID);

            if (existingMap != null)
            {
                existingMap.Vaccine_ID = student.Vaccine_ID ?? existingMap.Vaccine_ID;
                existingMap.Vaccinated_On = student.Vaccination_Date ?? existingMap.Vaccinated_On;
                _context.StudentVaccinations.Update(existingMap);
            }
            else if (student.Vaccine_ID.HasValue && student.Vaccination_Date.HasValue)
            {
                var newMap = new StudentVaccination
                {
                    Student_ID = student.Student_ID,
                    Vaccine_ID = student.Vaccine_ID.Value,
                    Vaccinated_On = student.Vaccination_Date.Value
                };
                _context.StudentVaccinations.Add(newMap);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}
