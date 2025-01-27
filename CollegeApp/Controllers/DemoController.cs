using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //1. Strongly Typed/ tightly coupled
        //private readonly IMyLogger _myLogger;
        //public DemoController()
        //{
        //    _myLogger = new LogToDB();
        //}

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    _myLogger.Log("Index method started");
        //    return Ok();
        //}

        //2. Loosely coupled/ loosely typed
        private readonly IMyLogger _myLogger;
        public DemoController(IMyLogger myLogger)
        {
            _myLogger = myLogger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.Log("Index method started");
            return Ok();
        }
    }
}
