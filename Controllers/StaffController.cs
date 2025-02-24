using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Repair_Notification_System.Models;
using RPS_DB.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Repair_Notification_System.Controllers
{
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;
        private readonly ApplicationDBContext _context; // Use your correct DbContext

        public StaffController(ILogger<StaffController> logger, ApplicationDBContext context)
        {
            _logger = logger;
            _context = context; // Now your database is ready to use later
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminAccountManager()
        {
            var AdminAccountManager = _context.Users.Where(t=>t.UserRole == UserRole.Admin).ToList();
            return View(AdminAccountManager);
        }

        public IActionResult AdminAccountAdd()
        {
            return View();
        }

        public IActionResult AdminAccountEdit()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        // Agency Manager View
        public IActionResult AgencyManager()
        {
            // Retrieve all agencies from the database
            var agencies = _context.Agencies.ToList();

            // Pass the list to the view
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
                // Log the exception (you can replace this with a proper logger)
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
                // Log the exception (you can replace this with a proper logger)
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

        // Ticket Manager
        public IActionResult TicketManager()
        {
            var TicketManager = _context.Tickets.Where(t=> t.State != TicketState.ดำเนินการเสร็จสิ้น).ToList();
            return View(TicketManager);
        }

        // Edit Ticket Manager
        public IActionResult TicketManagerEdit()
        {
            return View();
        }

    public IActionResult FinishTicket()
    {
        var finishedTickets = _context.Tickets.Where(t => t.State == TicketState.ดำเนินการเสร็จสิ้น).ToList();
        return View(finishedTickets);
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



        // Report
        public IActionResult Report()
        {
            return View();
        }

        // Error Handling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
