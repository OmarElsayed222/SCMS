using System;
using System.Collections.Generic;

namespace SCMS.ViewModels
{
    public class ReceptionSlotRowVm
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = "";

        public double Price { get; set; }
        public int Capacity { get; set; }
        public int BookedCount { get; set; }
        public int Remaining => Math.Max(0, Capacity - BookedCount);

        public string Status { get; set; } = "";
        public bool CanBook => Status == "Available" && Remaining > 0;
    }

    public class ReceptionSlotsListVm
    {
        public string? SearchTerm { get; set; }
        public DateTime? Date { get; set; }
        public List<ReceptionSlotRowVm> Slots { get; set; } = new();
    }
}
