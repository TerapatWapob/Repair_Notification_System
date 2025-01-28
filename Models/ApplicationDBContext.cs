using Microsoft.EntityFrameworkCore;
using RPS_DB.Models;

namespace Repair_Notification_System
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data for Agency
            modelBuilder.Entity<Agency>().HasData(
                new Agency
                {
                    ID = 1,
                    AgencyName = "Customer Support"
                },
                new Agency
                {
                    ID = 2,
                    AgencyName = "IT Department"
                }
            );

            // Seed Data for User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    ID = 1,
                    Name = "System Admin",
                    Position = "System Administrator",
                    PhoneNumber = "0000000000",
                    Username = "SystemAdmin",
                    Password = "SystemAdmin", // NOTE: Hash this in production!
                    Class = UserRole.SystemAdmin
                },
                new User
                {
                    ID = 2,
                    Name = "Test Admin",
                    Position = "Administrator",
                    PhoneNumber = "0000000000",
                    Username = "Admin",
                    Password = "Admin", // NOTE: Hash this in production!
                    Class = UserRole.Admin
                }
            );

            // Seed Data for Ticket
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    ID = 1,
                    Name = "John Doe",
                    Email = "johndoe@example.com",
                    PhoneNumber = "1234567890",
                    StartDate = new DateTime(2025, 1, 1),
                    EndDate = new DateTime(2025, 1, 5),
                    Topic = "Login Issue",
                    TypeOfProblem = "Authentication",
                    ProblemDescription = "Unable to log into the system.",
                    State = TicketState.รับเรื่องร้องเรียน,
                    AgencyID = 1
                },
                new Ticket
                {
                    ID = 2,
                    Name = "Jane Smith",
                    Email = "janesmith@example.com",
                    PhoneNumber = "0987654321",
                    StartDate = new DateTime(2025, 1, 10),
                    EndDate = new DateTime(2025, 1, 12),
                    Topic = "System Crash",
                    TypeOfProblem = "Technical",
                    ProblemDescription = "System crashes when opening the dashboard.",
                    State = TicketState.อยู่ระหว่างดำเนินการ,
                    AgencyID = 2
                }
            );
        }
    }
}
