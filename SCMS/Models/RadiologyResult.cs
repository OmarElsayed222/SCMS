using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCMS.Models
{
    public class RadiologyResult
    {
        [Key]
        public int ResultId { get; set; }

        [Required]
        [ForeignKey(nameof(Request))]
        public int RequestId { get; set; }

        [Required]
        [ForeignKey(nameof(Radiologist))]
        public int RadiologistId { get; set; }

        public string? ImagePath { get; set; }

        [Required]
        public string Report { get; set; } = null!;

        public string Status { get; set; } = "Completed";

        public DateTime ResultDate { get; set; } = DateTime.UtcNow;

        public RadiologyRequest Request { get; set; } = null!;
        public Radiologist Radiologist { get; set; } = null!;

        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    }
}
