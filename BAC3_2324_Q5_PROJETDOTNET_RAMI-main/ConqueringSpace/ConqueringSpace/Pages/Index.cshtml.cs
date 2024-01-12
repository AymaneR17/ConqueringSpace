using ConqueringSpace.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConqueringSpace.Interfaces;

namespace ConqueringSpace.Pages
{
    public class IndexModel : PageModel
    {
     
        public static User userConnected { get; set; }

        private readonly ILogger<IndexModel> _logger;
        ICelestialObjectService _celestialObjectService;
       

        List<CelestialObject> celestialObjects = new();

        public  IndexModel(ILogger<IndexModel> logger, ICelestialObjectService celestialObjectService )
        {
            _logger = logger;
            _celestialObjectService = celestialObjectService;
        }

        public async Task getCelestialObjectList()
        {

         
            celestialObjects = await _celestialObjectService.GetAll();
        }


        public async Task OnGet()
        {
            await getCelestialObjectList();

            if (userConnected != null)
            {
                ViewData["ConnectedUser"] = userConnected;
            }
            ViewData["CelestialObjectList"] = celestialObjects;
            Console.WriteLine("La liste : " + celestialObjects.Count());
        }
    }
}
