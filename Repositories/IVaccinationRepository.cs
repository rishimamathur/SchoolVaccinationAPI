using Microsoft.AspNetCore.Mvc;
using SchoolVaccinationAPI.Models;

namespace SchoolVaccinationAPI.Repositories
{
    public interface IVaccinationRepository
    {
        Task<IEnumerable<Vaccination>> GetAllVaccinations();
        Task<Vaccination?> GetVaccinationById(int id);
        Task AddVaccination(Vaccination vaccination);
        Task UpdateVaccination(Vaccination vaccination);
        Task DeleteVaccination(int id);
    }
}
