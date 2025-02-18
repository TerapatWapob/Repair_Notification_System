using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using RPS_DB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repair_Notification_System.Models;

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
        public IActionResult Index(string searchTerm)
        {
            // Start querying the tickets
            var ticketsQuery = _context.Tickets.AsQueryable();

            // Apply search if there's a search term
            if (!string.IsNullOrEmpty(searchTerm))
            {
                ticketsQuery = ticketsQuery.Where(t => t.Name.Contains(searchTerm));
            }

            // Sort by StartDate (Descending) - latest tickets first
            ticketsQuery = ticketsQuery.OrderByDescending(t => t.StartDate);

            // Get all tickets
            var tickets = ticketsQuery.ToList();

            // Set the searchTerm in ViewData to keep it after the form submission
            ViewData["SearchTerm"] = searchTerm;

            // Pass the tickets to the view
            return View(tickets);
        }

    public IActionResult Repair()
    {
        var agencies = _context.Agencies
            .Where(a => a.AgencyState) // Filter only active agencies
            .Select(a => new SelectListItem 
            { 
                Value = a.ID.ToString(), 
                Text = a.AgencyName 
            })
            .ToList();

        ViewBag.Agencies = agencies;
        return View();
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RepairSubmit(Ticket ticket)
    {
        if (!ModelState.IsValid)
        {
            var agencies = _context.Agencies
                .Select(a => new SelectListItem { Value = a.ID.ToString(), Text = a.AgencyName })
                .ToList();
            ViewBag.Agencies = agencies;
            return View("Repair", ticket);
        }

        // Debugging: Check if data is being received properly
        Console.WriteLine($"AgencyID: {ticket.AgencyID}, Topic: {ticket.Topic}, Email: {ticket.Email}");

        // Ensure AgencyID is valid
        var agencyExists = _context.Agencies.Any(a => a.ID == ticket.AgencyID);
        if (!agencyExists)
        {
            ModelState.AddModelError("AgencyID", "Invalid agency selection.");
            var agencies = _context.Agencies
                .Select(a => new SelectListItem { Value = a.ID.ToString(), Text = a.AgencyName })
                .ToList();
            ViewBag.Agencies = agencies;
            return View("Repair", ticket);
        }

        try
        {
            // Add the ticket to the context
            _context.Tickets.Add(ticket);

            // Save the changes to the database
            var result = _context.SaveChanges();
            
            // Debugging: Check the number of records affected
            Console.WriteLine($"Number of changes saved: {result}");
            
            TempData["SuccessMessage"] = "ข้อมูลของคุณถูกส่งเรียบร้อยแล้ว!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Log any errors during the save operation
            Console.WriteLine("Database Save Error: " + ex.Message);
            ModelState.AddModelError("", "An error occurred while saving data.");
        }

        // If we reach here, an error occurred
        var updatedAgencies = _context.Agencies
            .Select(a => new SelectListItem { Value = a.ID.ToString(), Text = a.AgencyName })
            .ToList();
        ViewBag.Agencies = updatedAgencies;
        return View("Repair", ticket);
    }

        public IActionResult Tutorial()
        {
            return View();
        }

        public IActionResult ContactUS()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
