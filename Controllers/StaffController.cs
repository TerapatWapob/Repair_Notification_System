using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Repair_Notification_System.Models;
using RPS_DB.Models;
using Microsoft.Extensions.Logging;

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
            return View();
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
            return View();
        }

        // Edit Ticket Manager
        public IActionResult TicketManagerEdit()
        {
            return View();
        }

        // Finish Ticket
        public IActionResult FinishTicket()
        {
            return View();
        }

        // Finish Ticket Detail
        public IActionResult FinishTicketDetail()
        {
            return View();
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
