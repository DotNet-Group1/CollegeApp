using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {
        }
        DbSet<Student> tbl_Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    StudentName = "John",
                    Email = "jonh@gmail.com",
                    Address = "Mumbai",
                    DOB = new DateTime(2023, 12, 12)
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Amit",
                    Email = "amit@gmail.com",
                    Address = "Kolkata",
                    DOB = new DateTime(2022, 11, 12)
                }
            });
        }

    }
}
 