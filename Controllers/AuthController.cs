using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolVaccinationAPI.Models;
using SchoolVaccinationAPI.Services;

namespace SchoolVaccinationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (AuthService.ValidateUser(request.Username, request.Password))
            {
                return Ok(new { token = "fake-jwt-token" });
            }
            return Unauthorized("Invalid credentials");
        }
    }

}
