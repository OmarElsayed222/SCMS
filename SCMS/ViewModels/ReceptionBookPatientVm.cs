using System.ComponentModel.DataAnnotations;

namespace SCMS.ViewModels
{
    public class ReceptionBookPatientVm
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public int AppointmentId { get; set; }
    }
}
