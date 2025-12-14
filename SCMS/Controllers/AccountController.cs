using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCMS.Models;
using SCMS.ViewModels;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace SCMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // ================= REGISTER =================
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVm());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (_context.Users.Any(u => u.Username == vm.Username))
            {
                ModelState.AddModelError("", "Username already exists");
                return View(vm);
            }

            var passwordHash = HashPassword(vm.Password);

            User user = vm.UserType switch
            {
                "Patient" => new Patient(),
                "Doctor" => new Doctor(),
                "Receptionist" => new Receptionist(),
                "Admin" => new Admin(),
                _ => new User()
            };

            user.FullName = vm.FullName;
            user.Email = vm.Email;
            user.Phone = vm.Phone;
            user.Username = vm.Username;
            user.PasswordHash = passwordHash;
            user.IsActive = true;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Login));
        }

        // ================= LOGIN =================
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVm());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    (u.Email == vm.EmailOrUsername || u.Username == vm.EmailOrUsername)
                    && u.IsActive);

            if (user == null || !VerifyPassword(vm.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(vm);
            }

            var discriminator = _context.Entry(user)
                .Property("Discriminator")
                .CurrentValue?.ToString() ?? "User";

            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("UserType", discriminator);

            return discriminator switch
            {
                "Admin" => RedirectToAction("Dashboard", "Admin"),
                "Doctor" => RedirectToAction("Dashboard", "Doctor"),
                "Receptionist" => RedirectToAction("Dashboard", "Reception"),
                "Radiologist" => RedirectToAction("Requests", "Radiology"),
                "Patient" => RedirectToAction("Index", "Home"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        // ================= PASSWORD =================
        private string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                100000,
                32));

            return $"{Convert.ToBase64String(salt)}.{hash}";
        }

        private bool VerifyPassword(string password, string hash)
        {
            var parts = hash.Split('.');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var stored = parts[1];

            var computed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                100000,
                32));

            return computed == stored;
        }
    }
}
