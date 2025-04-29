namespace SchoolVaccinationAPI.Services
{
    public static class AuthService
    {
        public static bool ValidateUser(string username, string password)
        {
            // Simulated user validation (could be extended with Roles table)
            return username == "admin" && password == "password123";
        }
    }
}
