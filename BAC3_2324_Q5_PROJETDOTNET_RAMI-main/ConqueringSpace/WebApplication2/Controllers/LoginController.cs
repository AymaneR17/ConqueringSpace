using System;
using System.Threading.Tasks;
using ConqueringSpace.Interfaces;
using ConqueringSpace.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ConqueringSpace.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IMemoryCache memoryCache;

        User? connectedUser;
        string token;


        public LoginController(IUsersService usersService, IMemoryCache memoryCache)
        {
            this.usersService = usersService;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            return View("Pages/Index.cshtml");
        }

        
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                var dateTime = DateTime.UtcNow.AddMinutes(10);

                List<Object> loginResult = await usersService.Login(user.Name, user.Password);

                if (loginResult != null)
                {                   
                    if (!memoryCache.TryGetValue("token", out string? token) )
                    {
                        connectedUser = loginResult[1] as User;
                        token = loginResult[0] as string;

                        memoryCache.Set("token", token, new DateTimeOffset(dateTime));
                        memoryCache.Set("user", connectedUser, new DateTimeOffset(dateTime));
                    }

                    ViewData["ConnectedUser"] = connectedUser;

                    return RedirectToPage("/Index", connectedUser);
                }

                // La connexion a échoué.
                ModelState.AddModelError(string.Empty, "Échec de la connexion.");
                return RedirectToPage("/Login"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite dans ConnexionController: {ex.Message}");
                ModelState.AddModelError(string.Empty, $"Erreur inattendue: {ex.Message}");
                return RedirectToPage("/Login"); 
            }
        }
    }
}
