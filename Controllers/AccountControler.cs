using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{

    [Route("api/account")]
    [ApiController]
    public class AccountControler : ControllerBase
    {
        private readonly IAccountServices _accountServices;

        public AccountControler(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountServices.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto)
        {
            string token = _accountServices.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
