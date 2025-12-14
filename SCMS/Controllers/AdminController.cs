using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCMS.Models;
using SCMS.ViewModels;

namespace SCMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var vm = new AdminDashboardVm
            {
                AdminName = "Admin",
                TotalUsers = await _context.Users.CountAsync(),
                TotalDoctors = await _context.Set<Doctor>().CountAsync(),
                TotalPatients = await _context.Set<Patient>().CountAsync(),
                TodayAppointmentsCount = await _context.Appointments
                    .CountAsync(a => a.AppointmentDate.Date == DateTime.Today),
                RecentUsers = await _context.Users
                    .OrderByDescending(u => u.UserId)
                    .Take(5)
                    .Select(u => new UserSummaryVm
                    {
                        UserId = u.UserId,
                        FullName = u.FullName,
                        Email = u.Email,
                        UserType = EF.Property<string>(u, "Discriminator"),
                        DateAdded = u.CreatedAt
                    })
                    .ToListAsync()
            };

            return View(vm);
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users
                .OrderBy(u => u.FullName)
                .Select(u => new UserSummaryVm
                {
                    UserId = u.UserId,
                    FullName = u.FullName,
                    Email = u.Email,
                    UserType = EF.Property<string>(u, "Discriminator"),
                    DateAdded = u.CreatedAt
                })
                .ToListAsync();

            return View(users);
        }
    }
}
