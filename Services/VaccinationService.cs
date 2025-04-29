using Microsoft.AspNetCore.Mvc;
using SchoolVaccinationAPI.Models;
using SchoolVaccinationAPI.Repositories;

namespace SchoolVaccinationAPI.Services
{
    public class VaccinationService : IVaccinationService
    {
        private readonly IVaccinationRepository _repository;
        public VaccinationService(IVaccinationRepository repository) => _repository = repository;

        public Task<IEnumerable<Vaccination>> GetAllVaccinations() => _repository.GetAllVaccinations();

        public Task<Vaccination?> GetVaccinationById(int id) => _repository.GetVaccinationById(id);

        public Task AddVaccination(Vaccination vaccination) => _repository.AddVaccination(vaccination);

        public Task UpdateVaccination(Vaccination vaccination) => _repository.UpdateVaccination(vaccination);

        public Task DeleteVaccination(int id) => _repository.DeleteVaccination(id);
    }
}
