using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Repair_Notification_System.Models;
using RPS_DB.Models;

namespace Repair_Notification_System.Controllers;

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
    
    public IActionResult AgencyManager()
    {
        // Retrieve all agencies from the database
        var agencies = _context.Agencies.ToList();
        
        // Pass the list to the view
        return View(agencies);
    }

    public IActionResult UpdateAgencyName(long agencyId, string newName)
    {
        var agency = _context.Agencies.Find(agencyId);
        if (agency != null)
        {
            agency.AgencyName = newName;
            _context.SaveChanges();
        }
        return Ok();
    }

    public IActionResult DisableAgency(long agencyId)
    {
        var agency = _context.Agencies.Find(agencyId);
        if (agency != null)
        {
            agency.AgencyState = false;
            _context.SaveChanges();
        }
        return Ok();
    }

    public IActionResult AddAgency(string agencyName)
    {
        var newAgency = new Agency
        {
            AgencyName = agencyName,
            AgencyState = true
        };
        _context.Agencies.Add(newAgency);
        _context.SaveChanges();
        return Ok(newAgency); // Return the new agency for rendering
    }











    
    public IActionResult TicketManager()
    {
        return View();
    }
    public IActionResult TicketManagerEdit()
    {
        return View();
    }
    
    public IActionResult FinishTicket()
    {
        return View();
    }
    
    public IActionResult FinishTicketDetail()
    {
        return View();
    }
      public IActionResult Report()
    {
        return View();
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
