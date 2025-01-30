using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data
{
    public class CollegeDBContext: DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {
        }
        DbSet<Student> tbl_Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Student>().ToTable("tbl_Students");
            modelBuilder.Entity<Student>().HasData(new List<Student>()
            {
                new Student {
                    Id = 1,
                    StudentName = "John",
                    Email = "jonh@gmail.com",
                    Address = "India",
                    DOB = new DateTime(2022, 12, 12)
                },
                new Student {
                    Id = 2,
                    StudentName = "Amit",
                    Email = "amit@gmail.com",
                    Address = "Delhi India",
                    DOB = new DateTime(2020, 02, 11)
                },
                new Student {
                    Id = 3,
                    StudentName = "Sumit",
                    Email = "sumit@gmail.com",
                    Address = "WB,India",
                    DOB = new DateTime(2021, 10, 10)
                }
            });
        }
    }
}
 