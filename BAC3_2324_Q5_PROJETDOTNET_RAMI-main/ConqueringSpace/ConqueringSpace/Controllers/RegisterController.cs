using ConqueringSpace.Interfaces;
using ConqueringSpace.Model;
using ConqueringSpace.Pages;
using ConqueringSpace.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConqueringSpace.Controllers
{

    public class RegisterController : Controller
    {
        private readonly IUsersService usersService;

        public RegisterController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        
        public IActionResult Index()
        {
           
            return View("Pages/Index.cshtml");
        }

        
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                var registrationResult = await usersService.Register(user.Name, user.Password, user.Role = "1"); 

                // 1 = user conncté donc peut placer des objets et visualier/ etre notifié. 
                // 2 peut faire ce que fait le 2 avec en plus le Admin
                Console.WriteLine($"Registration result: {registrationResult}");


                if (registrationResult.Equals("OK"))
                {
                    // L'enregistrement a réussi.
                    Console.WriteLine(" Enregistrement effectué !");

                    return RedirectToPage("/Index");
                }

                // L'enregistrement a échoué.
                ModelState.AddModelError(string.Empty, "Register error.");
                return RedirectToPage("/Register"); // Correction ici, utilisez le chemin complet de la page Razor
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Une erreur s'est produite dans RegisterController: {ex.Message}");
                ModelState.AddModelError(string.Empty, $"Unexpected error: {ex.Message}");
                return RedirectToPage("/Register");
            }
        }
    }
}
