using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace RPS_DB.Models
{
public enum AgencyStatus
{
    Enabled,
    Disabled
}

public class Agency
{
    public Agency()
    {
        Tickets = new List<Ticket>();
    }

    [Key]
    public long ID { get; set; }

    [MaxLength(100)]
    public required string AgencyName { get; set; }
    // AgencyState to indicate enable/disable (true = enabled, false = disabled)
    public bool AgencyState { get; set; } = true;

    public virtual ICollection<Ticket> Tickets { get; set; }
}


    public class Ticket
    {
        [Key]
        public long ID { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        public required string Email { get; set; }

        [MaxLength(15)]
        [Phone]
        public required string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [MaxLength(100)]
        public required string Topic { get; set; }

        [MaxLength(100)]
        public required string TypeOfProblem { get; set; }

        public required string ProblemDescription { get; set; }
        public string? AgentComment { get; set; }

        [Required]
        public TicketState State { get; set; }

        public long AgencyID { get; set; }

        [ForeignKey("AgencyID")]
        public Agency? Agency { get; set; }
    }

    public enum TicketState
    {
        รับเรื่องร้องเรียน,
        อยู่ระหว่างดำเนินการ,
        ดำเนินการเสร็จสิ้น
    }

    public class User
    {
        [Key]
        public long ID { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(50)]
        public required string Position { get; set; }

        [MaxLength(15)]
        [Phone]
        public required string PhoneNumber { get; set; }

        [MaxLength(50)]
        [Required]
        public required string Username { get; set; }

        [MaxLength(255)]
        [Required]
        public required string Password { get; set; }

        [Required]        
        public UserRole UserRole { get; set; }
    }

    public enum UserRole
    {
        SystemAdmin,
        Admin
    }
}
