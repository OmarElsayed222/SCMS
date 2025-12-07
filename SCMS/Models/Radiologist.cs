using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCMS.Models
{
    public class Radiologist
    {
        [Key]
        public int RadiologistId { get; set; }
        [Required]
        [ForeignKey(nameof(Staff))]
        public int StaffId { get; set; }

        public Staff Staff { get; set; } = null!;
        public ICollection<RadiologyRequest> RadiologyRequests { get; set; } = new List<RadiologyRequest>();
        public ICollection<RadiologyResult> RadiologyResults { get; set; } = new List<RadiologyResult>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    
}
}
