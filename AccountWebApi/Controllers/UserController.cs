using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public ActionResult GetUsers()
        {
            return Ok("Daniel");
        }
    }
}
