using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCMS.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }
        [Required]
        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        [Required]
        [ForeignKey(nameof(Doctor))]
        public int DoctorId { get; set; }
        [Required]
        public string Diagnosis { get; set; } = null!;
        [Required]
        public string Treatment { get; set; } = null!;
        public bool RadiologyRequested { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;

        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        public ICollection<RadiologyRequest> RadiologyRequest { get; set; } = new List<RadiologyRequest>();
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    
}
}
