using CollegeApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly CollegeDBContext _context;

        public DatabaseController(CollegeDBContext context)
        {
            _context = context;
        }

        [HttpGet("test-connection")]
        public IActionResult TestDatabaseConnection()
        {
            try
            {
                if (_context.Database.CanConnect())
                {
                    return Ok("Database connection successful!");
                }
                else
                {
                    return StatusCode(500, "Database connection failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
