using SchoolVaccinationAPI.Models;

namespace SchoolVaccinationAPI.Repositories
{
    public interface IDashboardRepository
    {
        Task<DashboardResponse> GetDashboardSummaryAsync();
    }
}
