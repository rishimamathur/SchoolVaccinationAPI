using SchoolVaccinationAPI.Models;
using SchoolVaccinationAPI.Repositories;

namespace SchoolVaccinationAPI.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repository;
        public DashboardService(IDashboardRepository repository) => _repository = repository;

        public Task<DashboardResponse> GetDashboardSummaryAsync() => _repository.GetDashboardSummaryAsync();
    }
}
