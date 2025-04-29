using Microsoft.EntityFrameworkCore;
using SchoolVaccinationAPI.Data;
using SchoolVaccinationAPI.Models;

namespace SchoolVaccinationAPI.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardResponse> GetDashboardSummaryAsync()
        {
            var totalStudents = await _context.Students.CountAsync();
            var vaccinatedCount = await _context.Students.CountAsync(s => s.Vaccination_Status == true);

            var vaccinatedPercent = totalStudents > 0
                ? Math.Round((double)vaccinatedCount / totalStudents * 100, 2)
                : 0;

            var today = DateTime.Today;

            var upcomingDrives = await _context.Vaccinations
                .Where(v => v.Date_of_Drive >= today)
                .OrderBy(v => v.Date_of_Drive)
                .Select(v => new VaccinationDriveInfo
                {
                    Id = v.Vaccine_ID,
                    VaccineName = v.Vaccine_Name,
                    Date = v.Date_of_Drive
                })
                .ToListAsync();

            return new DashboardResponse
            {
                TotalStudents = totalStudents,
                VaccinatedCount = vaccinatedCount,
                VaccinatedPercent = vaccinatedPercent,
                UpcomingDrives = upcomingDrives
            };
        }
    }
}
