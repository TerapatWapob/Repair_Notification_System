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
                    Status = AgencyStatus.Enabled
                },
                new Agency
                {
                    ID = 2,
                    AgencyName = "คณะครุศาสตร์",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 3,
                    AgencyName = "คณะเทคโนโลยีอุตสาหกรรม",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 4,
                    AgencyName = "คณะบริหารธุรกิจและการจัดการ",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 5,
                    AgencyName = "คณะมนุษยศาสตร์และสังคมศาสตร์",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 6,
                    AgencyName = "คณะวิทยาศาสตร์",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 7,
                    AgencyName = "คณะพยาบาลศาสตร์",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 8,
                    AgencyName = "คณะแพทย์แผนไทยและทางเลือก",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 9,
                    AgencyName = "คณะนิติศาสตร์",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 10,
                    AgencyName = "คณะพยาบาลศาสตร์",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 11,
                    AgencyName = "คณะสาธารณสุขศาสตร์",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 12,
                    AgencyName = "คณะวิทยาการคอมพิวเตอร์",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 13,
                    AgencyName = "บัณทิตวิทยาลัย",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 14,
                    AgencyName = "โรงเรียนสาธิตมหาวิทยาลัยราชภัฏอุบลราชธานี",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 15,
                    AgencyName = "สำนักส่งเสริมวิชาการและงานทะเบียน",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 16,
                    AgencyName = "สำนักศิลปะและวัฒนธรรม",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 17,
                    AgencyName = "สำนักวิทยบริการและเทคโนโลยีสารสนเทศ",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 18,
                    AgencyName = "สถาบันวิจัยและพัฒนา",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 19,
                    AgencyName = "สำนักบริการวิชาการชุมชน",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 20,
                    AgencyName = "สำนักงานตรวจสอบภายใน",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 21,
                    AgencyName = "สำนักงานอธิการบดี",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 22,
                    AgencyName = "กองกลาง (-งานบริหารทั่วไป)",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 23,
                    AgencyName = "กองกลาง (-งานสถาปัตยกรรมและสาธารณูปโภค)",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 24,
                    AgencyName = "กองกลาง (-งานสิ่งแวดล้อมและภูมิทัศน์)",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 25,
                    AgencyName = "กองกลาง (-งานอาคารสถานที่)",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 26,
                    AgencyName = "กองกลาง (-งานยานพาหนะ)",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 27,
                    AgencyName = "กองกลาง (-งานรักษาความปลอดภัยและจราจร)",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 28,
                    AgencyName = "กองนโยบายและแผน",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 29,
                    AgencyName = "กองบริหารงานบุคคล",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 30,
                    AgencyName = "กองพัฒนานักศึกษา",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 31,
                    AgencyName = "กองคลัง",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 32,
                    AgencyName = "กองสวัสดิการ",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 33,
                    AgencyName = "ศูนย์คอมพิวเตอร์",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 34,
                    AgencyName = "ศูนย์พัฒนากีฬาและสุขภาพ",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 35,
                    AgencyName = "สำนักงานส่งเสริมมหาวิทยาลัย",
                    Status = AgencyStatus.Enabled
                }
                ,
                new Agency
                {
                    ID = 36,
                    AgencyName = "กองเลขานุการ สำนักงานอธิการบดี",
                    Status = AgencyStatus.Enabled
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
                    StartDate = new DateTime(2025, 1, 8),
                    EndDate = new DateTime(2025, 1, 12),
                    Topic = "System Crash",
                    TypeOfProblem = "Technical",
                    ProblemDescription = "System crashes when opening the dashboard.",
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
                    TypeOfProblem = "Technical",
                    ProblemDescription = "System crashes when opening the dashboard.",
                    State = TicketState.เสร็จสิ้น,
                    AgencyID = 2
                }
            );
        }
    }
}
