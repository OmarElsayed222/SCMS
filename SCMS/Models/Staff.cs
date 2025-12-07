namespace SCMS.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public int UserId { get; set; }

        public string EmployeeName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public double Salary { get; set; }

        public User User { get; set; } = null!;
        public Doctor? Doctor { get; set; }
        public Radiologist? Radiologist { get; set; }
        public Receptionist? Receptionist { get; set; }
        public Admin? Admin { get; set; }
    }
}
