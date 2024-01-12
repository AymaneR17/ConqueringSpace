using ApiAuthentification.DTO;
using ApiAuthentification.Interface;
using ApiAuthentification.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiAuthentification.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly ILoginService connectionService;
        private readonly IConfiguration configuration;

        public LoginController(ILoginService connectionService, IConfiguration configuration)
        {
            this.connectionService = connectionService;
            this.configuration = configuration;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserConnectionDTO userConnectionDTO)
        {
            var user = connectionService.Login(userConnectionDTO.Name, userConnectionDTO.Password);

            if (user != null)
            {
                var claim = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Name),

                };
                var token = connectionService.GenerateToken(configuration["Jwt:Key"], claim);

                return Ok(token);

            }
            return Unauthorized(); 

        }


        [HttpGet("AllUsers")]
        public ActionResult<List<User>> GetAllUsers()
        {
            return connectionService.GetAll();
        }
       
     
    }
}
