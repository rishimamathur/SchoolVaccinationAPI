using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolVaccinationAPI.Models
{
    [Table("Student_Master")]
    public class Student
    {
        [Key]
        public int Student_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Class { get; set; }
        public bool Vaccination_Status { get; set; }
        public DateTime? Vaccination_Date { get; set; }

        [NotMapped]
        public int? Vaccine_ID { get; set; }

        public ICollection<StudentVaccination>? StudentVaccinations { get; set; }
    }
}
