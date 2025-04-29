using Microsoft.EntityFrameworkCore;
using SchoolVaccinationAPI.Models;
using System.Collections.Generic;

namespace SchoolVaccinationAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<StudentVaccination> StudentVaccinations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentVaccination>()
                .ToTable("Student_Vaccination_Map")
                .HasKey(sv => sv.ID);
        }
    }
}
