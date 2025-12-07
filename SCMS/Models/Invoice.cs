using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCMS.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        [Required]
        [ForeignKey(nameof(Appointment))]
        public int AppointmentId { get; set; }
        [Required]
        public double TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Appointment Appointment { get; set; } = null!;
    }
}
