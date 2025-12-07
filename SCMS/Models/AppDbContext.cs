using Microsoft.EntityFrameworkCore;

namespace SCMS.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Staff> Staff { get; set; } = null!;
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Radiologist> Radiologists { get; set; } = null!;
        public DbSet<Receptionist> Receptionists { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<Prescription> Prescriptions { get; set; } = null!;
        public DbSet<MedicalRecord> MedicalRecords { get; set; } = null!;
        public DbSet<Feedback> Feedbacks { get; set; } = null!;
        public DbSet<RadiologyRequest> RadiologyRequests { get; set; } = null!;
        public DbSet<RadiologyResult> RadiologyResults { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Patient)
                .WithOne(p => p.User)
                .HasForeignKey<Patient>(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Staff)
                .WithOne(s => s.User)
                .HasForeignKey<Staff>(s => s.UserId);

            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Doctor)
                .WithOne(d => d.Staff)
                .HasForeignKey<Doctor>(d => d.StaffId);

            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Radiologist)
                .WithOne(r => r.Staff)
                .HasForeignKey<Radiologist>(r => r.StaffId);

            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Receptionist)
                .WithOne(rc => rc.Staff)
                .HasForeignKey<Receptionist>(rc => rc.StaffId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Invoice)
                .WithOne(i => i.Appointment)
                .HasForeignKey<Invoice>(i => i.AppointmentId);

            modelBuilder.Entity<RadiologyRequest>()
                .HasOne(r => r.Result)
                .WithOne(res => res.Request)
                .HasForeignKey<RadiologyResult>(res => res.RequestId);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Patient)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(f => f.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Doctor)
                .WithMany(d => d.Feedbacks)
                .HasForeignKey(f => f.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany(pt => pt.Prescriptions)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RadiologyRequest>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.RadiologyRequests)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.MedicalRecords)
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Restrict);


            foreach (var fk in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                // مالناش دعوة بالـ Owned types
                if (!fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                {
                    fk.DeleteBehavior = DeleteBehavior.Restrict;   
                }
            }
        }
    }
}
