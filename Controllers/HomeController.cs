using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repair_Notification_System.Models;  // Ensure this is here
using Microsoft.EntityFrameworkCore;
using System;
using RPS_DB.Models;
using Microsoft.AspNetCore.Mvc.Rendering; // Add this line

namespace Repair_Notification_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Index
        public IActionResult Index()
        {
            var tickets = _context.Tickets.ToList();  // Fetch the list of tickets from the database
            return View(tickets);
        }

        // GET: Repair (this is the page where the user adds a ticket)
        public IActionResult Repair()
        {
            try
            {
                // Fetching agencies from the database
                var agencies = _context.Agencies.ToList();
                
                // Check if agencies were retrieved and populate ViewBag
                if (agencies != null && agencies.Any())
                {
                    ViewBag.Agencies = new SelectList(agencies, "Id", "Name");  // Assuming "Id" is the agency ID and "Name" is the display name
                }
                else
                {
                    ViewBag.Agencies = new List<SelectListItem>();  // If no agencies, provide an empty list
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading agencies.");
                return View("Error"); // Return to an error page if something goes wrong
            }

            return View();
        }

        // POST: Repair (this is the action that processes the submitted form)
        [HttpPost]
        [ValidateAntiForgeryToken] // Helps prevent CSRF attacks
        public IActionResult Repair(Ticket ticket)  // We can directly use Ticket as parameter
        {
            if (!ModelState.IsValid)  // If the form data is invalid
            {
                return View(ticket);
            }
            _context.Tickets.Add(ticket);  // Add the ticket to the context
            _context.SaveChanges();  // Save the changes to the database

            return RedirectToAction("Index");  // Redirect to the Index action (ticket list)
        }

        public IActionResult Tutorial()
        {
            return View();
        }

        public IActionResult ContactUS()
        {
            return View();
        }

        // Error action (for handling errors globally)
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
