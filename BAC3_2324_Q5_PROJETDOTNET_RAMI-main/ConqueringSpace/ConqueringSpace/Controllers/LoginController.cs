using System;
using System.Threading.Tasks;
using ConqueringSpace.Interfaces;
using ConqueringSpace.Model;
using ConqueringSpace.Pages;
using Microsoft.AspNetCore.Authorization;
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



        public IActionResult Logout()
        {
            memoryCache.Set("token", token, new DateTimeOffset(DateTime.Now));
            token = null;
            return RedirectToPage("/Login");
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

                    IndexModel.userConnected = connectedUser;
                    AdminModel.userConnected = connectedUser;


                    ViewData["ConnectedUser"] = connectedUser;

                    return RedirectToPage("/Index");
                }

                // La connexion a échoué.
                ModelState.AddModelError(string.Empty, "Connexion error");
                return RedirectToPage("/Login"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured on login controller: {ex.Message}");
                ModelState.AddModelError(string.Empty, $"Error : {ex.Message}");
                return RedirectToPage("/Login"); 
            }
        }
    }
}
