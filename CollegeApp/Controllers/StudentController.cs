using CollegeApp.Data;
using CollegeApp.Models;
using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly CollegeDBContext _dbContext;
        public StudentController(ILogger<StudentController> logger, CollegeDBContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;
        }

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

            _logger.LogInformation("Get Student method started");
            var students = _dbContext.tbl_Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Address = s.Address,
                Email = s.Email,
                DOB = s.DOB
            }).ToList();
            //var students = _dbContext.tbl_Students.ToList();
            return Ok(students);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))] it can be use if Student object is not required inside the method: after ActionResult
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{Id:int}", Name = "getStudentById")]
        public ActionResult<StudentDTO> GetStudentById(int Id)
        {
            //BadRequest - 400 - BadRequest - Client error
            if (Id <= 0)
            {
                _logger.LogWarning("BadRequest");
                return BadRequest();
            }                

            var student = _dbContext.tbl_Students.Where(n => n.Id == Id).FirstOrDefault();

            //NotFound - 404 - NotFound - Client error
            if (student == null)
            {
                _logger.LogError("Student not found with given id");
                return NotFound($"The student with id {Id} not found");
            }
            var studentDTO = new Student
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email,
                DOB = student.DOB
            };
            //OK - 200 - Success
            return Ok(studentDTO);
        }

        [HttpDelete("Delete/{Id}", Name = "deleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int Id)
        {
            //BadRequest - 400 - BadRequest - Client error
            if (Id <= 0)
                return BadRequest();

            var student = _dbContext.tbl_Students.Where(n => n.Id == Id).FirstOrDefault();

            //NotFound - 404 - NotFound - Client error
            if (student == null)
                return NotFound($"The student with id {Id} not found");

            _dbContext.tbl_Students.Remove(student);
            _dbContext.SaveChanges();
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

            var student = _dbContext.tbl_Students.Where(n => n.StudentName == name).FirstOrDefault();

            //NotFound - 404 - NotFound - Client error
            if (student == null)
                return NotFound($"The student with name {name} not found");

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email,
                DOB = student.DOB
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

            Student student = new Student
            {
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email,
                DOB = model.DOB
            };
            _dbContext.tbl_Students.Add(student);
            _dbContext.SaveChanges();

            model.Id = student.Id;
            return CreatedAtRoute("getStudentById", new { model.Id }, model);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            if (!ModelState.IsValid || model == null)
                return BadRequest(ModelState);

            var student = _dbContext.tbl_Students.AsNoTracking().Where(n => n.Id == model.Id).FirstOrDefault();
            
            if (student == null)
                return NotFound($"The student with id {model.Id} not found");

            var newRecord = new Student()
            {
                Id = model.Id,
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address,
                DOB = model.DOB
            };
            _dbContext.tbl_Students.Update(newRecord);

            //student.StudentName = model.StudentName;
            //student.Address = model.Address;
            //student.Email = model.Email;
            //student.DOB = model.DOB;

            _dbContext.SaveChanges();

            return Ok(model);
        }

        [HttpPatch]
        [Route("{Id:int}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudentPartial(int Id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (!ModelState.IsValid || patchDocument == null || Id <= 0)
                return BadRequest(ModelState);

            var student = _dbContext.tbl_Students.Where(n => n.Id == Id).FirstOrDefault();

            if (student == null)
                return NotFound($"The student with id {Id} not found");

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email,
                DOB = student.DOB
            };
            patchDocument.ApplyTo(studentDTO, ModelState);
            if (!ModelState.IsValid) 
                return BadRequest();

            student.StudentName = studentDTO.StudentName;
            student.Address = studentDTO.Address;
            student.Email = studentDTO.Email;
            student.DOB = studentDTO.DOB;
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
