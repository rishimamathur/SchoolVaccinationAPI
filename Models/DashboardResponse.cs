namespace SchoolVaccinationAPI.Models
{
    public class DashboardResponse
    {
        public int TotalStudents { get; set; }
        public int VaccinatedCount { get; set; }
        public double VaccinatedPercent { get; set; }
        public List<VaccinationDriveInfo> UpcomingDrives { get; set; } = new();
    }

    public class VaccinationDriveInfo
    {
        public int Id { get; set; }
        public string VaccineName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
