public class TicketDetailViewModel
{
    public required string Topic { get; set; }
    public required string? AgencyName { get; set; }
    public required string Email { get; set; }
    public required string TypeOfProblem { get; set; }
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public DateTime StartDate { get; set; }  // Non-nullable DateTime
    public DateTime EndDate { get; set; }    // Non-nullable DateTime
    public required string ProblemDescription { get; set; }
    public string? AgentComment { get; set; }
}
