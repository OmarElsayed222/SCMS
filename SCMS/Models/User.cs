using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SCMS.Models
{
    public class User 
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(150)]
        public string FullName { get; set; } = null!;
        [Required, MaxLength(150)]
        public string Email { get; set; } = null!;
        [Required, MaxLength(100)]
        public string Username { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;
        [MaxLength(20)]
        public string Phone { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Patient? Patient { get; set; }
        public Staff? Staff { get; set; }



    }
}
