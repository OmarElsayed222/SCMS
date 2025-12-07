using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCMS.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        [Required]
        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        [Required]
        [ForeignKey(nameof(Doctor))]
        public int DoctorId { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; }     
        
        public string? FeedbackText { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;

        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
    }
}
