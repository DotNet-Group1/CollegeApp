using CollegeApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "getAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            ////OK - 200 - Success
            //return Ok(CollegeRepository.Students);

            //var students = new List<StudentDTO>();
            //foreach(var item in CollegeRepository.Students)
            //{
            //    StudentDTO obj = new StudentDTO()
            //    {
            //        id = item.id,
            //        StudentName = item.StudentName,
            //        Address = item.Address,
            //        Email = item.Email
            //    };
            //    students.Add(obj);
            //}

            var students = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                id = s.id,
                StudentName = s.StudentName,
                Address = s.Address,
                Email = s.Email
            });
            return Ok(students);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))] it can be use if Student object is not required inside the method: after ActionResult
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id:int}", Name = "getStudentById")]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            //BadRequest - 400 - BadRequest - Client error
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.id == id).FirstOrDefault();

            //NotFound - 404 - NotFound - Client error
            if (student == null)
                return NotFound($"The student with id {id} not found");

            var studentDTO = new StudentDTO
            {
                id = student.id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email
            };
            //OK - 200 - Success
            return Ok(studentDTO);
        }

        [HttpDelete("{id}", Name = "deleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            //BadRequest - 400 - BadRequest - Client error
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.id == id).FirstOrDefault();

            //NotFound - 404 - NotFound - Client error
            if (student == null)
                return NotFound($"The student with id {id} not found");

            CollegeRepository.Students.Remove(student);
            //OK - 200 - Success 
            return Ok(true);
        }

        [HttpGet("{name:alpha}", Name = "getStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            //BadRequest - 400 - BadRequest - Client error
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();

            //NotFound - 404 - NotFound - Client error
            if (student == null)
                return NotFound($"The student with name {name} not found");

            var studentDTO = new StudentDTO
            {
                id = student.id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email
            };
            //OK - 200 - Success
            return Ok(studentDTO);
        }
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model) 
        { 
            if (!ModelState.IsValid || model == null) 
                return BadRequest(ModelState);
            
            //if(model.AdmissionDate < DateTime.Now)
            //{
            //    //1. Directly adding error message to model state
            //    //2.  Using custom attribute to add error message
            //    ModelState.AddModelError("AdmissionDate", "Admission date should be greater than current date");
            //    return BadRequest(ModelState);
            //}

            int newId = CollegeRepository.Students.LastOrDefault().id + 1;
            Student student = new Student
            {
                id = newId,
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email
            };
            CollegeRepository.Students.Add(student);
            model.id = student.id;
            return CreatedAtRoute("getStudentById", new { model.id }, model);
        }
    }
}
