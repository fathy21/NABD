using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NABD.Areas.Identity;
using NABD.Models.Domain;

namespace NABD.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Emergency> Emergencies { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<MedicalHistory> MedicalHistory { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<PatientGuardian> PatientGuardians { get; set; }
        public DbSet<PatientDoctor> PatientDoctors { get; set; }
        public DbSet<MQTTMessage> MQTTMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Emergency Relationships
            modelBuilder.Entity<Emergency>()
                .HasOne(e => e.Patient)
                .WithMany(p => p.Emergencies)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Emergency>()
                .HasOne(e => e.Tool)
                .WithMany(t => t.Emergencies)
                .HasForeignKey(e => e.ToolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Emergency>()
                .HasOne(e => e.Doctor)
                .WithMany(ms => ms.Emergencies)
                .HasForeignKey(e => e.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Emergency>()
                .HasOne(e => e.Nurse)
                .WithMany(ms => ms.Emergencies)
                .HasForeignKey(e => e.NurseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Emergency>()
                .HasOne(e => e.Guardian)
                .WithMany(g => g.Emergencies)
                .HasForeignKey(e => e.GuardianId)
                .OnDelete(DeleteBehavior.Restrict);

            // Guardian - Patient M:M
            modelBuilder.Entity<PatientGuardian>()
                .HasKey(pg => new { pg.PatientId, pg.GuardianId });

            modelBuilder.Entity<PatientGuardian>()
                .HasOne(pg => pg.Patient)
                .WithMany(p => p.PatientGuardians)
                .HasForeignKey(pg => pg.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PatientGuardian>()
                .HasOne(pg => pg.Guardian)
                .WithMany(g => g.PatientGuardians)
                .HasForeignKey(pg => pg.GuardianId)
                .OnDelete(DeleteBehavior.Cascade);

            // Doctor - Patient M:M
            modelBuilder.Entity<PatientDoctor>()
                .HasKey(pd => new { pd.PatientId, pd.DoctorId });

            modelBuilder.Entity<PatientDoctor>()
                .HasOne(pd => pd.Patient)
                .WithMany(p => p.PatientDoctors)
                .HasForeignKey(pd => pd.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PatientDoctor>()
                .HasOne(pd => pd.Doctor)
                .WithMany(d => d.PatientDoctors)
                .HasForeignKey(pd => pd.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            // MedicalHistory Relationship
            modelBuilder.Entity<MedicalHistory>()
                .HasOne(mh => mh.Patient)
                .WithOne(p => p.MedicalHistory)
                .HasForeignKey<MedicalHistory>(mh => mh.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Patient - Report Relationship
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.Reports)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor - Report Relationship
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Doctor)
                .WithMany(ms => ms.Reports)
                .HasForeignKey(r => r.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tool - Patient 1:1 Relationship
            modelBuilder.Entity<Tool>()
                .HasOne(t => t.Patient)
                .WithOne(p => p.Tool)
                .HasForeignKey<Tool>(t => t.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Doctor - Nurse Relationship
            modelBuilder.Entity<Nurse>()
                .HasOne(n => n.Doctor)
                .WithMany(d => d.Nurses)
                .HasForeignKey(n => n.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Notification Relationships
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Patient)
                .WithMany(p => p.Notifications)
                .HasForeignKey(n => n.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Guardian)
                .WithMany(g => g.Notifications)
                .HasForeignKey(n => n.GuardianId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Tool)
                .WithMany(t => t.Notifications)
                .HasForeignKey(n => n.ToolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Doctor)
                .WithMany(ms => ms.Notifications)
                .HasForeignKey(n => n.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Nurse)
                .WithMany(ms => ms.Notifications)
                .HasForeignKey(n => n.NurseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tool - MqttMessage 1:M Relationship
            modelBuilder.Entity<MQTTMessage>()
                .HasOne(n => n.Tool)
                .WithMany(t => t.MQTTMessages)
                .HasForeignKey(n => n.ToolId)
                .OnDelete(DeleteBehavior.Restrict);


            // Seeding Roles
            var userRoleId = "2e39ce08-64e1-40c4-b25a-ab98e1345be0";
            var medicalStaffRoleId = "4e5bd3e5-6cf5-4b08-9c41-ccc761fb6de1"; // Fixed the typo
            var guardianRoleId = "b8da6f82-e554-4f87-8b8b-f61e9891bd33";

            var roles = new List<IdentityRole>
            {
              new IdentityRole
              {
                  Id = userRoleId,
                  ConcurrencyStamp = userRoleId,
                  Name = "User",
                  NormalizedName = "USER"
              },
              new IdentityRole
              {
                  Id = medicalStaffRoleId,
                  ConcurrencyStamp = medicalStaffRoleId,
                  Name = "MedicalStaff", // Fixed the typo
                  NormalizedName = "MEDICALSTAFF"
              },
              new IdentityRole
              {
                  Id = guardianRoleId,
                  ConcurrencyStamp = guardianRoleId,
                  Name = "Guardian",
                  NormalizedName = "GUARDIAN"
              }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
