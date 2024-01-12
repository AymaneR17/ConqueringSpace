using ApiConqueringSpace.DTO;
using ApiConqueringSpace.Interface;
using ApiConqueringSpace.Model;
using ApiConqueringSpace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiConqueringSpace.Controllers
{
    public class CelestialObjectController : ControllerBase
    {
        private readonly ICelestialObjectService celestialObjectService;

        public CelestialObjectController(ICelestialObjectService celestialObjectService)
        {
            this.celestialObjectService = celestialObjectService;
        }



        [HttpPost("CreateCelestialObject")]
        public IActionResult CreateCelestialObject([FromBody] CelestialObjectDTO celestialObjectDTO)
        {

            string result = celestialObjectService.Create(celestialObjectDTO.Name,
                                                          celestialObjectDTO.Image,
                                                          celestialObjectDTO.PositionX,
                                                          celestialObjectDTO.PositionY);
            if (result == "OK")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var response = celestialObjectService.Delete(id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }





        [HttpPut("Update")]
        public IActionResult UpdateCelestialObject([FromBody] CelestialObject celestialObject)
        {

            string result = celestialObjectService.Update(celestialObject.Id,
                                                          celestialObject.Name,
                                                          celestialObject.Image,
                                                          celestialObject.PositionX,
                                                          celestialObject.PositionY);
            if (result == "OK")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpGet("AllCelestialObject")]
        public ActionResult<List<CelestialObject>> GetAllCelestialObject()
        {
            return celestialObjectService.GetAll();
        }
    }
}
