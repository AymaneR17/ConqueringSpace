using ApiAuthentification.DTO;
using ApiAuthentification.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuthentification.Controllers
{
    public class DeleteController : ControllerBase
    {
        private readonly IDeleteService deleteService;

        public DeleteController(IDeleteService deleteService)
        {
            this.deleteService = deleteService;
        }


        [Authorize]
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var response = deleteService.Delete(id);
            if (response != null)
            {
                return Ok(response);    
            }
            return BadRequest();
        }
    }
}
