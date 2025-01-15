using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Repair_Notification_System.Models;

namespace Repair_Notification_System.Controllers;

public class StaffController : Controller
{
    private readonly ILogger<StaffController> _logger;

    public StaffController(ILogger<StaffController> logger)
    {
        _logger = logger;
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
    
    public IActionResult AdminAcccountEdit()
    {
        return View();
    }
    
    public IActionResult Profile()
    {
        return View();
    }
    
    public IActionResult AgencyManager()
    {
        return View();
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
