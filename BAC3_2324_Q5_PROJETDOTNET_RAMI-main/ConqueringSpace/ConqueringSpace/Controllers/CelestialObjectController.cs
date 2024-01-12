using ConqueringSpace.Interfaces;
using ConqueringSpace.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ConqueringSpace.Controllers
{
    public class CelestialObjectController : Controller
    {
        private readonly ICelestialObjectService celestialObjectService;

        public CelestialObjectController(ICelestialObjectService celestialObjectService)
        {
            this.celestialObjectService = celestialObjectService;
        }

        [Authorize]
        public IActionResult Index()
        {
            
            string connectedUserName = User.Identity.Name;
                ViewBag.ConnectedUserName = connectedUserName;
                return View("Pages/Index.cshtml");
        }


        public async Task<IActionResult> CreateCelestialObject(CelestialObject celestialObject)
        {
            string img = "";
            try {

                if (celestialObject.Name.Equals("meteor"))
                {
                   img = "/lib/meteor.png";
                }
                if (celestialObject.Name.Equals("satellite"))
                {
                   img = "/lib/satellite.png";
                }
                if (celestialObject.Name.Equals("spiral"))
                {
                   img = "/lib/spiral.png";
                }
                if (celestialObject.Name.Equals("stars"))
                {
                    img = "/lib/stars.png";
                }
                if (celestialObject.Name.Equals("alien"))
                {
                    img = "/lib/alien.png";
                }

                var result =  await celestialObjectService.CreateCelestialObject(celestialObject.Name,
                                                                         celestialObject.Image = img,
                                                                          celestialObject.PositionX =-100,
                                                                          celestialObject.PositionY =-100);

                if (result.Equals("OK"))
                {
                    RedirectToPage("/Admin");
                }


                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error : {ex.Message}");
                return RedirectToPage("/Index");
            }
        }



        public async Task<IActionResult> DeleteCelestialObject(string name)
        {
            try
            {
                List<CelestialObject> celestialObjectsList = await celestialObjectService.GetAll();

                foreach (var item in celestialObjectsList)
                {
                    if (item.Name == name)
                    {
                        await celestialObjectService.DeleteCelestialObject(item.Id);
                    }
                }

                return RedirectToPage("/Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error : {ex.Message}");
                return RedirectToPage("/Index");
            }
        }

        public async Task<IActionResult> UpdateCelestialObject(CelestialObject celestialObject)
        {
            try
            {
                string img = "";
                int posX;
                int posY;

                if (celestialObject.Name.Equals("meteor"))
                {
                    img = "/lib/meteor.png";
                }
                if (celestialObject.Name.Equals("satellite"))
                {
                    img = "/lib/satellite.png";
                }
                if (celestialObject.Name.Equals("spiral"))
                {
                    img = "/lib/spiral.png";
                }
                if (celestialObject.Name.Equals("stars"))
                {
                    img = "/lib/stars.png";
                }
                if (celestialObject.Name.Equals("alien"))
                {
                    img = "/lib/alien.png";
                }

                var result = await celestialObjectService.UpdateCelestialObject(celestialObject.Id,
                                                                                celestialObject.Name,
                                                                                celestialObject.Image = img,
                                                                                celestialObject.PositionX,
                                                                                celestialObject.PositionY);

                if (result.Equals("OK"))
                {
                    return RedirectToPage("/Admin");
                }

                ModelState.AddModelError(string.Empty, "Update failed.");
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                return RedirectToPage("/Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PlaceCelestialObject(CelestialObject celestialObject)
        {
            try
            {

                var selectedCelestialObject  = await celestialObjectService.GetById(celestialObject.Id);
               
                Console.WriteLine("Celestial object :" + selectedCelestialObject.Name);

                if (selectedCelestialObject == null)
                {
                    return NotFound();
                }

                selectedCelestialObject.PositionX = celestialObject.PositionX;
                selectedCelestialObject.PositionY = celestialObject.PositionY;
       
         

                await celestialObjectService.UpdateForUser(selectedCelestialObject);

                // Stockez les valeurs de position dans ViewData
                TempData["SelectedCelestialObjectPositionX"] = selectedCelestialObject.PositionX;
                TempData["SelectedCelestialObjectPositionY"] = selectedCelestialObject.PositionY;

                ViewBag.SelectedCelestialObjectPositionX = selectedCelestialObject.PositionX;
                ViewBag.SelectedCelestialObjectPositionY = selectedCelestialObject.PositionY;
                // Rediriger vers l'action Index
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while placing celestial object: {ex.Message}");
                return BadRequest("An error occurred while placing celestial object.");
            }
        }

    }
}
