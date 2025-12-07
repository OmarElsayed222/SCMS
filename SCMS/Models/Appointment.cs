using System.ComponentModel.DataAnnotations;

namespace SCMS.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        
        public int? RadiologistId { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        public double Price { get; set; }
        [Required]
        public string Status { get; set; } ="Pending"; 
        public string? DiagnosisSummary { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Patient Patient { get; set; } = null!;
        public Doctor? Doctor { get; set; }
        public Radiologist? Radiologist { get; set; }
        public Receptionist? Receptionist { get; set; }

        public Invoice? Invoice { get; set; }
    }
}
