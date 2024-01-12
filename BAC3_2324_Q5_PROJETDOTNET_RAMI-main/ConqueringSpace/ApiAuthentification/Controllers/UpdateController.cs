using ApiAuthentification.DTO;
using ApiAuthentification.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuthentification.Controllers
{
    public class UpdateController : ControllerBase
    {
        private readonly IUpdateService updateService;

        public UpdateController(IUpdateService updateService)
        {
            this.updateService = updateService;
        }


        [HttpPut("Update")]
        public IActionResult Update([FromBody] UserUpdateDTO userDTO)
        {

            if (!ModelState.IsValid) // verifier si le model n'est pas respecte
            {
                return BadRequest(ModelState);
            }
            string result = updateService.Update(userDTO.Id,userDTO.Name, userDTO.Role); // Update fait

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
