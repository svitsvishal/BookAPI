using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {

        }

        [HttpGet]
        public  ActionResult Get()
        {
            return Ok("this code ");

        }
    }
}
