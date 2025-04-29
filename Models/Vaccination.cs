using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SchoolVaccinationAPI.Models
{
    [Table("Vaccination_Master")]
    public class Vaccination
    {
        [Key]
        public int Vaccine_ID { get; set; }
        public string Vaccine_Name { get; set; }
        public DateTime Date_of_Drive { get; set; }
        public int Available_Doses { get; set; }
        public string Applicable_Classes { get; set; }

        [JsonIgnore]
        public ICollection<StudentVaccination>? StudentVaccinations { get; set; }
    }
}
