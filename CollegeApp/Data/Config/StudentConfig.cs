using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
           builder.ToTable("tbl_Students");
           builder.HasKey(n => n.Id);

           builder.Property(n => n.Id).UseIdentityColumn();
           builder.Property(n => n.StudentName).IsRequired().HasMaxLength(250);
           builder.Property(n => n.Email).HasMaxLength(250);
           builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
               
           builder.HasData(new List<Student>()
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
                    DOB = new DateTime(2022, 3, 11)
                },
               new Student
                {
                    Id = 3,
                    StudentName = "Sumit",
                    Email = "sumit@gmail.com",
                    Address = "Delhi",
                    DOB = new DateTime(2020, 2, 10)
                },
               new Student
                {
                    Id = 4,
                    StudentName = "Abijit",
                    Email = "abijit@gmail.com",
                    Address = "North 24 Parganas",
                    DOB = new DateTime(2015, 9, 08)
                }
           });
        }
    }
}
