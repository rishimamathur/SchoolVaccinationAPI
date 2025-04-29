using System.ComponentModel.DataAnnotations;

namespace SchoolVaccinationAPI.Models
{
    public class UploadStudentCsvRequest
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
