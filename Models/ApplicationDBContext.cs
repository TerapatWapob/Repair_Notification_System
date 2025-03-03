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
                    AgencyName = "คณะเกษตรศาสตร์",
                    AgencyState = true
                },
                new Agency
                {
                    ID = 2,
                    AgencyName = "คณะครุศาสตร์",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 3,
                    AgencyName = "คณะเทคโนโลยีอุตสาหกรรม",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 4,
                    AgencyName = "คณะบริหารธุรกิจและการจัดการ",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 5,
                    AgencyName = "คณะมนุษยศาสตร์และสังคมศาสตร์",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 6,
                    AgencyName = "คณะวิทยาศาสตร์",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 7,
                    AgencyName = "คณะพยาบาลศาสตร์",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 8,
                    AgencyName = "คณะแพทย์แผนไทยและทางเลือก",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 9,
                    AgencyName = "คณะนิติศาสตร์",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 10,
                    AgencyName = "คณะพยาบาลศาสตร์",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 11,
                    AgencyName = "คณะสาธารณสุขศาสตร์",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 12,
                    AgencyName = "คณะวิทยาการคอมพิวเตอร์",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 13,
                    AgencyName = "บัณทิตวิทยาลัย",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 14,
                    AgencyName = "โรงเรียนสาธิตมหาวิทยาลัยราชภัฏอุบลราชธานี",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 15,
                    AgencyName = "สำนักส่งเสริมวิชาการและงานทะเบียน",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 16,
                    AgencyName = "สำนักศิลปะและวัฒนธรรม",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 17,
                    AgencyName = "สำนักวิทยบริการและเทคโนโลยีสารสนเทศ",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 18,
                    AgencyName = "สถาบันวิจัยและพัฒนา",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 19,
                    AgencyName = "สำนักบริการวิชาการชุมชน",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 20,
                    AgencyName = "สำนักงานตรวจสอบภายใน",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 21,
                    AgencyName = "สำนักงานอธิการบดี",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 22,
                    AgencyName = "กองกลาง (-งานบริหารทั่วไป)",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 23,
                    AgencyName = "กองกลาง (-งานสถาปัตยกรรมและสาธารณูปโภค)",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 24,
                    AgencyName = "กองกลาง (-งานสิ่งแวดล้อมและภูมิทัศน์)",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 25,
                    AgencyName = "กองกลาง (-งานอาคารสถานที่)",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 26,
                    AgencyName = "กองกลาง (-งานยานพาหนะ)",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 27,
                    AgencyName = "กองกลาง (-งานรักษาความปลอดภัยและจราจร)",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 28,
                    AgencyName = "กองนโยบายและแผน",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 29,
                    AgencyName = "กองบริหารงานบุคคล",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 30,
                    AgencyName = "กองพัฒนานักศึกษา",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 31,
                    AgencyName = "กองคลัง",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 32,
                    AgencyName = "กองสวัสดิการ",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 33,
                    AgencyName = "ศูนย์คอมพิวเตอร์",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 34,
                    AgencyName = "ศูนย์พัฒนากีฬาและสุขภาพ",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 35,
                    AgencyName = "สำนักงานส่งเสริมมหาวิทยาลัย",
                    AgencyState = true
                }
                ,
                new Agency
                {
                    ID = 36,
                    AgencyName = "กองเลขานุการ สำนักงานอธิการบดี",
                    AgencyState = true
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
                    Password = "399e93cb2b3f1d89dca97a9e1dc57b3688bab7352a4133f0305eec2996e03d78", // NOTE: Hash this in production!
                    UserRole = UserRole.SystemAdmin
                },
                new User
                {
                    ID = 2,
                    Name = "Test Admin",
                    Position = "Administrator",
                    PhoneNumber = "0000000000",
                    Username = "Admin",
                    Password = "c1c224b03cd9bc7b6a86d77f5dace40191766c485cd55dc48caf9ac873335d6f", // NOTE: Hash this in production!
                    UserRole = UserRole.Admin
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
                    StartDate = new DateTime(2025, 1, 15),
                    EndDate = null,
                    Topic = "Login Issue",
                    TypeOfProblem = "ปัญหาเกี่ยวกับฮาร์ดแวร์/อุปกรณ์",
                    ProblemDescription = "Unable to log into the system.",
                    AgentComment ="Test",
                    State = TicketState.รับเรื่องร้องเรียน,
                    AgencyID = 1
                },
                new Ticket
                {
                    ID = 2,
                    Name = "Jane Smith",
                    Email = "janesmith@example.com",
                    PhoneNumber = "0987654321",
                    StartDate = new DateTime(2025, 2, 10),
                    EndDate = null,
                    Topic = "System Crash",
                    TypeOfProblem = "ปัญหาเกี่ยวกับฮาร์ดแวร์/อุปกรณ์",
                    ProblemDescription = "System crashes when opening the dashboard.",
                    AgentComment ="Test",
                    State = TicketState.อยู่ระหว่างดำเนินการ,
                    AgencyID = 2
                },
                new Ticket
                {
                    ID = 3,
                    Name = "Luthor Smith",
                    Email = "Luthor@example.com",
                    PhoneNumber = "0892835021",
                    StartDate = new DateTime(2025, 1, 9),
                    EndDate = new DateTime(2025, 1, 12),
                    Topic = "System Crash",
                    TypeOfProblem = "ปัญหาเกี่ยวกับซอฟต์แวร์",
                    ProblemDescription = "System crashes when opening the dashboard.",
                    AgentComment ="Test",
                    State = TicketState.ดำเนินการเสร็จสิ้น,
                    AgencyID = 2
                },
                new Ticket
                {
                    ID = 4,
                    Name = "Luthor Sama",
                    Email = "Luthor@example.com",
                    PhoneNumber = "0892835021",
                    StartDate = new DateTime(2025, 2, 15),
                    EndDate = new DateTime(2025, 2, 18),
                    Topic = "System Crash",
                    TypeOfProblem = "ปัญหาด้านเครือข่าย",
                    ProblemDescription = "System crashes when opening the dashboard.",
                    AgentComment ="Test",
                    State = TicketState.ดำเนินการเสร็จสิ้น,
                    AgencyID = 2
                },
                new Ticket
                {
                    ID = 5,
                    Name = "Luthor Sama",
                    Email = "Luthor@example.com",
                    PhoneNumber = "0892835021",
                    StartDate = new DateTime(2025, 2, 25),
                    EndDate = new DateTime(2025, 2, 27),
                    Topic = "System Crash",
                    TypeOfProblem = "อื่นๆ",
                    ProblemDescription = "System crashes when opening the dashboard.",
                    AgentComment ="Test",
                    State = TicketState.ดำเนินการเสร็จสิ้น,
                    AgencyID = 2
                }
            );
        }
    }
}
