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
    }
}
