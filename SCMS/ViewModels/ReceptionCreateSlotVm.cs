using System;
using System.ComponentModel.DataAnnotations;

namespace SCMS.ViewModels
{
    public class ReceptionCreateSlotVm
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Range(1, 500)]
        public int Capacity { get; set; } = 10;

        [Range(0, 100000)]
        public double Price { get; set; }

        [Required]
        public string Status { get; set; } = "Available";
    }
}
