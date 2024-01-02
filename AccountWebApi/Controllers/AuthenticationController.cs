using AccountWebApi.Model;
using AccountWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public AuthenticationController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var userToken = _tokenService.GenerateToken(user);

            if (userToken == null)
            {
                return Unauthorized();
            }

            return Ok(userToken);
        }
    }
}
