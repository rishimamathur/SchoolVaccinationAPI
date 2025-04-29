using Microsoft.EntityFrameworkCore;
using SchoolVaccinationAPI.Data;
using SchoolVaccinationAPI.Models;

namespace SchoolVaccinationAPI.Repositories
{
    public class VaccinationRepository : IVaccinationRepository
    {
        private readonly AppDbContext _context;
        public VaccinationRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Vaccination>> GetAllVaccinations() => await _context.Vaccinations.ToListAsync();

        public async Task<Vaccination?> GetVaccinationById(int id) => await _context.Vaccinations.FindAsync(id);

        public async Task AddVaccination(Vaccination vaccination)
        {
            _context.Vaccinations.Add(vaccination);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVaccination(Vaccination vaccination)
        {
            _context.Vaccinations.Update(vaccination);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVaccination(int id)
        {
            var vaccination = await _context.Vaccinations.FindAsync(id);
            if (vaccination != null)
            {
                _context.Vaccinations.Remove(vaccination);
                await _context.SaveChangesAsync();
            }
        }
    }
}
