using RPS_DB.Models;

public class TicketManagerViewModel
{
    public required List<Ticket> Tickets { get; set; }
    public int TotalTickets { get; set; }
    public int CompletedTickets { get; set; }
}
