using ApiAuthentification.DTO;
using ApiAuthentification.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace ApiAuthentification.Controllers
{
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService registerService;

        public RegisterController(IRegisterService registerService) {
            this.registerService = registerService;
        }


        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRegistrationDTO userDTO) {

            if (!ModelState.IsValid) // verifier si le model n'est pas respecte
            {
                return BadRequest(ModelState);
            }
           string result= registerService.Register(userDTO.Name, userDTO.Password, userDTO.Role); // enregistrement fait

            if (result == "OK")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
            
        }

    }
}
