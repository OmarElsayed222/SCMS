namespace SCMS.Models
{
    public class Receptionist
    {
        public int ReceptionistId { get; set; }
        public int StaffId { get; set; }

        public string Shift { get; set; } = null!;

        public Staff Staff { get; set; } = null!;
        public ICollection<Appointment> AppointmentsCreated { get; set; } = new List<Appointment>();
    }
}
