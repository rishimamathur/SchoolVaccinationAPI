using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SchoolVaccinationAPI.Models
{
    [Table("Student_Vaccination_Map")]
    public class StudentVaccination
    {
        [Key]
        public int ID { get; set; }
        public int Student_ID { get; set; }
        public int Vaccine_ID { get; set; }
        public DateTime Vaccinated_On { get; set; }

        [JsonIgnore]
        [ForeignKey("Student_ID")]
        public Student Student { get; set; }

        [JsonIgnore]
        [ForeignKey("Vaccine_ID")]
        public Vaccination Vaccination { get; set; }
    }
}
