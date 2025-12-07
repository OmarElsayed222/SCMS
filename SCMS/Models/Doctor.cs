using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCMS.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        [Required]
        [ForeignKey(nameof(Staff))]
        public int StaffId { get; set; }
        [Required]
        public string Specialization { get; set; } = null!;
        public int YearsOfExperience { get; set; }

        public Staff Staff { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}

