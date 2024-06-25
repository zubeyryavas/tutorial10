using Microsoft.EntityFrameworkCore;

namespace tutorial10.Models
{
    public class S20697Context : DbContext
    {
        private readonly IConfiguration _configuration;
        
        public S20697Context(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public S20697Context(DbContextOptions options) : base (options)
            {

            }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultDbCon"));
        }

        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<AuthUsers> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Medicament>(e =>
            {
                e.HasData(new Medicament { IdMedicament = 1, name = "Kemo", Description="dont Know", Type="Dont Know"});
            });
            
            builder.Entity<Doctor>(e =>
            {
                
                e.HasData(new Doctor {IdDoctor = 1, FirstName="Munna", LastName="Bhai", Email = "harsh@gmail.com" });
            });
            
            builder.Entity<Patient>(e =>
            {
                e.HasData(new Patient { IdPatient = 1, FirstName = "harsh", LastName = "Patel", Birthdate = new DateTime(2000,1,1)});
            });
            builder.Entity<Prescription>(e =>
            {
                e.HasData(new Prescription { Idprescription = 1, Date = new DateTime(2020,1,1), DueDate = new DateTime(2021,1,1), IdPatient =1, IdDoctor =1});
            });


            builder.Entity<Prescription_Medicament>(e =>
            {
                e.HasKey("IdMedicament");
                e.HasData(new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 3, Details = "dont know" });

            });
        }
    }
}
