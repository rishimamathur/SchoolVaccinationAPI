using Microsoft.AspNetCore.Mvc;
using SchoolVaccinationAPI.Models;
using SchoolVaccinationAPI.Repositories;

namespace SchoolVaccinationAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        public StudentService(IStudentRepository repository) => _repository = repository;

        public Task<IEnumerable<Student>> GetAllStudents() => _repository.GetAllStudents();

        public Task<Student?> GetStudentById(int id) => _repository.GetStudentById(id);

        public Task AddStudent(Student student) => _repository.AddStudent(student);

        public Task UpdateStudent(Student student) => _repository.UpdateStudent(student);

        public Task DeleteStudent(int id) => _repository.DeleteStudent(id);
    }
}
