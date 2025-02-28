using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Repair_Notification_System.Models;
using RPS_DB.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Repair_Notification_System.Controllers
{
    public static class PasswordHelper
{
    public static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}

    [Authorize]
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;
        private readonly ApplicationDBContext _context; // Use your correct DbContext

        public StaffController(ILogger<StaffController> logger, ApplicationDBContext context)
        {
            _logger = logger;
            _context = context; // Now your database is ready to use later
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetString("UserRole");

            if (!string.IsNullOrEmpty(userRole))
            {
                return RedirectToAction("TicketManager"); // Redirect if already logged in
            }

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Load users into memory first
            var users = _context.Users.ToList(); 

            // Hash the input password
            var hashedPassword = PasswordHelper.HashPassword(password);

            // Find user (case-sensitive comparison)
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);

            if (user != null)
            {
                string userRoleString = user.UserRole.ToString(); // Convert enum to string

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()), 
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, userRoleString) 
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                return RedirectToAction("TicketManager"); 
            }

            ViewBag.Error = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง";
            return View("Index"); 
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "SystemAdmin")]
        public IActionResult AdminAccountManager()
        {
            var AdminAccountManager = _context.Users.Where(user=>user.UserRole == UserRole.Admin).ToList();
            return View(AdminAccountManager);
        }
        [Authorize(Roles = "SystemAdmin")]
        public IActionResult AdminAccountAdd()
        {
            return View();
        }
        [Authorize(Roles = "SystemAdmin")]
        [HttpGet("Staff/AdminAccountEdit/{id}")]
        public IActionResult AdminAccountEdit(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View("AdminAccountEdit", user);
        }
        [Authorize(Roles = "SystemAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAdmin(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("AdminAccountManager");
        }
        [Authorize(Roles = "SystemAdmin")]
        [HttpPost]
        public IActionResult UpdateAdmin(User admin)
        {
            var existingAdmin = _context.Users.Find(admin.ID);
            if (existingAdmin != null)
            {
                // ✅ Only update fields if the new value is not empty
                if (!string.IsNullOrWhiteSpace(admin.Name))
                    existingAdmin.Name = admin.Name;

                if (!string.IsNullOrWhiteSpace(admin.Position))
                    existingAdmin.Position = admin.Position;

                if (!string.IsNullOrWhiteSpace(admin.PhoneNumber))
                    existingAdmin.PhoneNumber = admin.PhoneNumber;

                if (!string.IsNullOrWhiteSpace(admin.Username))
                    existingAdmin.Username = admin.Username;

                _context.SaveChanges();
            }

            return RedirectToAction("AdminAccountManager");
        }
        [Authorize(Roles = "SystemAdmin")]
        [HttpPost]
        public IActionResult ResetAdminPassword(long ID)
        {
            var admin = _context.Users.Find(ID);
            if (admin != null)
            {
                admin.Password = PasswordHelper.HashPassword("12345678"); // Hash before saving
                _context.SaveChanges();
            }
            return RedirectToAction("AdminAccountManager");
        }
        [Authorize(Roles = "SystemAdmin")]
        [HttpGet]
        public JsonResult CheckUsernameAvailability(string username)
        {
            bool isAvailable = !_context.Users.Any(u => u.Username == username);
            return Json(new { available = isAvailable });
        }
        [Authorize(Roles = "SystemAdmin")]
        [HttpPost]
        public IActionResult AddAdminAccount(User model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "ข้อมูลไม่ถูกต้อง โปรดตรวจสอบอีกครั้ง";
                return RedirectToAction("AdminAccountAdd");
            }

            if (_context.Users.Any(u => u.Username == model.Username))
            {
                TempData["ErrorMessage"] = "ชื่อบัญชีนี้ถูกใช้แล้ว";
                return RedirectToAction("AdminAccountAdd");
            }

            var newAdmin = new User
            {
                Name = model.Name,
                Position = model.Position,
                PhoneNumber = model.PhoneNumber,
                Username = model.Username,
                Password = PasswordHelper.HashPassword(model.Password), // Hash here
                UserRole = UserRole.Admin
            };
            _context.Users.Add(newAdmin);

            try
            {
                _context.SaveChanges();
                TempData["SuccessMessage"] = "เพิ่มบัญชีสำเร็จ!";
                return RedirectToAction("AdminAccountManager");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "เกิดข้อผิดพลาด: " + ex.Message;
                return RedirectToAction("AdminAccountAdd");
            }
        }

        public IActionResult Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index");
            }

            var user = _context.Users.FirstOrDefault(u => u.ID.ToString() == userId);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(string fullName, string position, string phone, string oldPassword, string newPassword, string confirmPassword)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index");
            }

            var user = _context.Users.FirstOrDefault(u => u.ID.ToString() == userId);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            bool isUpdated = false; // Flag to track changes
            bool isPasswordUpdated = false; // Flag to check password update

            // Update user details only if they are different
            if (!string.IsNullOrWhiteSpace(fullName) && user.Name != fullName)
            {
                user.Name = fullName;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(phone) && user.PhoneNumber != phone)
            {
                user.PhoneNumber = phone;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(position) && user.Position != position)
            {
                user.Position = position;
                isUpdated = true;
            }

            // Password Change Logic
            if (!string.IsNullOrEmpty(oldPassword) && !string.IsNullOrEmpty(newPassword))
            {
                if (user.Password != PasswordHelper.HashPassword(oldPassword))
                {
                    ViewBag.Error = "รหัสผ่านเดิมไม่ถูกต้อง";
                    return View("Profile", user);
                }

                if (newPassword.Length < 8)
                {
                    ViewBag.Error = "รหัสผ่านใหม่ต้องมีอย่างน้อย 8 ตัวอักษร";
                    return View("Profile", user);
                }

                if (newPassword != confirmPassword)
                {
                    ViewBag.Error = "รหัสผ่านใหม่ไม่ตรงกัน";
                    return View("Profile", user);
                }

                string hashedNewPassword = PasswordHelper.HashPassword(newPassword);

                if (user.Password != hashedNewPassword) // Only update if different
                {
                    user.Password = hashedNewPassword;
                    isUpdated = true;
                    isPasswordUpdated = true;
                }
            }

            // Save changes if there were updates
            if (isUpdated)
            {
                _context.SaveChanges();

                if (isPasswordUpdated)
                {
                    ViewBag.Success = "อัปเดตรหัสผ่านสำเร็จ";
                }
                else
                {
                    ViewBag.Success = "อัปเดตข้อมูลสำเร็จ";
                }
            }
            else
            {
                ViewBag.Info = "ไม่มีการเปลี่ยนแปลงข้อมูล"; // Show a neutral message
            }

            return View("Profile", user);
        }

        public IActionResult AgencyManager()
        {
            var agencies = _context.Agencies.ToList();
            return View(agencies);
        }

        [HttpPost]
        public IActionResult EditAgency([FromBody] Agency agency)
        {
            if (agency == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var existingAgency = _context.Agencies.Find(agency.ID);
                if (existingAgency == null)
                {
                    return NotFound("Agency not found.");
                }

                existingAgency.AgencyName = agency.AgencyName;
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public IActionResult AddAgency([FromBody] Agency agency)
        {
            if (agency == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                _context.Agencies.Add(agency);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public IActionResult DeleteAgency([FromBody] DeleteAgencyRequest request)
        {
            var agency = _context.Agencies.FirstOrDefault(a => a.ID == request.Id);
            if (agency != null)
            {
                agency.AgencyState = false;
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Agency not found." });
        }
        public IActionResult TicketManager(string searchTerm)
        {
            var tickets = _context.Tickets
                .Where(t => t.State != TicketState.ดำเนินการเสร็จสิ้น) // Exclude completed tickets
                .AsQueryable();

            // Filter by search term if provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                tickets = tickets.Where(t => t.Topic.Contains(searchTerm));
            }

            // Enum-based sorting (Primary: State, Secondary: Oldest StartDate first)
            var orderedTickets = tickets
                .OrderBy(t => t.State == TicketState.รับเรื่องร้องเรียน ? 1 :
                            t.State == TicketState.อยู่ระหว่างดำเนินการ ? 2 : 3) // No need for "ดำเนินการเสร็จสิ้น"
                .ThenBy(t => t.StartDate) // Oldest tickets appear first
                .ToList();

            return View(orderedTickets);
        }


        public IActionResult TicketManagerEdit(int id)
        {
            var ticket = _context.Tickets
                .Include(t => t.Agency) // If AgencyName is in a related table
                .FirstOrDefault(t => t.ID == id);
            if (ticket == null)
            {
                return NotFound(); // Show 404 if ticket doesn't exist
            }

            return View("TicketManagerEdit", ticket); // Load TicketManagerEdit.cshtml
        }
        public IActionResult DeleteTicket(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.ID == id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                _context.SaveChanges();
            }
            return RedirectToAction("TicketManager");
        }
        [HttpPost]
        public IActionResult MarkAsCompleted(int id)
        {
               var ticket = _context.Tickets.FirstOrDefault(t => t.ID == id);
             if (ticket != null)
            {
                ticket.State = TicketState.ดำเนินการเสร็จสิ้น;
                ticket.EndDate = DateTime.Today;
                _context.SaveChanges();
            }
            return RedirectToAction("TicketManager");
        }
        [HttpPost]
        public IActionResult UpdateTicketStatus(long id, string newStatus)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.ID == id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Convert newStatus (string) to TicketState (enum)
            if (Enum.TryParse(typeof(TicketState), newStatus, out var parsedState))
            {
                ticket.State = (TicketState)parsedState; // Assign the converted enum value

                if (ticket.State == TicketState.ดำเนินการเสร็จสิ้น) // Check if it's "Completed"
                {
                    ticket.EndDate = DateTime.Today; // Set EndDate to today
                    _context.SaveChanges();
                    return RedirectToAction("TicketManager"); // Redirect to TicketManager
                }

                _context.SaveChanges();
                return RedirectToAction("TicketManagerEdit", new { id }); // Stay in TicketManagerEdit
            }

            return BadRequest("Invalid status provided"); // Handle invalid values
        }
        [HttpPost]
        public IActionResult DeleteAllTickets()
        {
            var openTickets = _context.Tickets.Where(t => t.EndDate == null).ToList(); // Select only tickets without EndDate
            _context.Tickets.RemoveRange(openTickets);
            _context.SaveChanges();
            return RedirectToAction("TicketManager"); // Adjust to your actual view
        }

        [HttpPost]
        public IActionResult MarkAllAsCompleted()
        {
            var allTickets = _context.Tickets.ToList();
            foreach (var ticket in allTickets)
            {
                ticket.State = (TicketState)2; // Adjust this according to your actual status
                ticket.EndDate = DateTime.Now;
            }
            _context.SaveChanges();
            return RedirectToAction("TicketManager"); // Change this to your actual view
        }
        public IActionResult FinishTicket(string searchTerm)
        {
            var finishedTickets = _context.Tickets
                .Where(t => t.State == TicketState.ดำเนินการเสร็จสิ้น);

            // Filter by search term
            if (!string.IsNullOrEmpty(searchTerm))
            {
                finishedTickets = finishedTickets.Where(t => t.Topic.Contains(searchTerm));
            }

            // Sorting: Primary -> EndDate (Latest First), Secondary -> ID (Latest First)
            var orderedTickets = finishedTickets
                .OrderByDescending(t => t.EndDate ?? DateTime.MinValue)  // Newest EndDate first
                .ThenByDescending(t => t.ID)  // Newest ID first
                .ToList();

            return View(orderedTickets);
        }
        
        public IActionResult FinishTicketDetail(long id)
        {
            var ticket = _context.Tickets
                .Include(t => t.Agency)  // Ensure related data is loaded
                .FirstOrDefault(t => t.ID == id);

            if (ticket == null)
            {
                return NotFound();
            }

            // Ensure that StartDate and EndDate are not null
            if (!ticket.StartDate.HasValue || !ticket.EndDate.HasValue)
            {
                ticket.StartDate = ticket.StartDate ?? DateTime.Now;
                ticket.EndDate = ticket.EndDate ?? DateTime.Now;
            }

            var model = new TicketDetailViewModel
            {
                Topic = ticket.Topic,
                AgencyName = ticket.Agency?.AgencyName ?? "Unknown",
                Email = ticket.Email,
                TypeOfProblem = ticket.TypeOfProblem,
                Name = ticket.Name,
                PhoneNumber = ticket.PhoneNumber,
                StartDate = ticket.StartDate.Value, // Non-nullable
                EndDate = ticket.EndDate.Value,     // Non-nullable
                ProblemDescription = ticket.ProblemDescription
            };

            return View(model);
        }

        [HttpPost] // Use POST to prevent accidental deletions
        public IActionResult DeleteFinishTicket(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.ID == id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                _context.SaveChanges();
            }

            return RedirectToAction("FinishTicket"); // Redirect to the main page after deletion
        }

        [HttpPost]
        public IActionResult DeleteAllFinishTickets()
        {
            var completedTickets = _context.Tickets.Where(t => t.EndDate != null).ToList();

            if (completedTickets.Any())
            {
                _context.Tickets.RemoveRange(completedTickets);
                _context.SaveChanges();
            }

            return RedirectToAction("FinishTicket"); // Redirect to the same page
        }

        [Authorize]
        public IActionResult Report()
        {
            DateTime lastYear = DateTime.Now.AddDays(-365);

            // ✅ TypeOfProblem Pie Chart (Fixed)
            var typeOfProblemData = _context.Tickets
                .Where(t => t.StartDate >= lastYear && !string.IsNullOrEmpty(t.TypeOfProblem))
                .GroupBy(t => t.TypeOfProblem)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToList();

            // ✅ TicketState Pie Chart (Fixed: State is an enum, so we use ToString())
            var ticketStateData = _context.Tickets
                .Where(t => t.StartDate >= lastYear)
                .GroupBy(t => t.State)
                .Select(g => new { State = g.Key.ToString(), Count = g.Count() }) // ✅ Use ToString()
                .ToList();

            // ✅ Top 10 Agencies Bar Chart (Fixed)
            var agencyData = _context.Tickets
                .Where(t => t.StartDate >= lastYear)
                .Join(
                    _context.Agencies,
                    t => t.AgencyID, 
                    a => a.ID, 
                    (t, a) => a.AgencyName // ✅ Change this to the actual column name in `Agencies`
                )
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .GroupBy(name => name.ToLower().Trim())
                .Select(g => new { Agency = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(10)
                .ToList();

            ViewBag.TypeOfProblemData = typeOfProblemData;
            ViewBag.TicketStateData = ticketStateData;
            ViewBag.AgencyData = agencyData;

            return View();
        }
    }

        // Request models for Add, Edit, and Delete Agency
        public class EditAgencyRequest
        {
            public int Id { get; set; }
            public required string NewName { get; set; }
        }

        public class DeleteAgencyRequest
        {
            public int Id { get; set; }
        }

        public class AddAgencyRequest
        {
            public required string NewName { get; set; }
        }
    }