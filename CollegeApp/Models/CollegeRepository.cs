namespace CollegeApp.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>()
        {
            new Student
            {
                id = 1,
                StudentName = "Amit",
                Email = "amit@gmail.com",
                Address ="Kolkata"
            },
            new Student
            {
                id = 2,
                StudentName = "Sumit",
                Email = "sumit@gmail.com",
                Address = "Nodia"
            },
            new Student
            {
                id = 3,
                StudentName = "Subho",
                Email = "subho@gmail.com",
                Address = "Burnpur"
            },
            new Student
            {
                id = 4,
                StudentName = "Abir",
                Email = "abir@gmail.com",
                Address = "Daimond Harber"
            }

            //new Student
            //{
            //    id = 4,
            //    StudentName = "Abir",
            //    Email = "abir@gmail.com",
            //    Address = "Daimond Harber"
            //}
        };
    }
}
