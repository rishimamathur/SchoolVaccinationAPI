using SchoolVaccinationAPI.Models;

namespace SchoolVaccinationAPI.Services
{
    public interface IDashboardService
    {
        Task<DashboardResponse> GetDashboardSummaryAsync();
    }
}
