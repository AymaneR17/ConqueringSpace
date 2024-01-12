using ConqueringSpace.Interfaces;
using ConqueringSpace.Model;
using Microsoft.AspNetCore.Mvc;

namespace ConqueringSpace.Controllers
{
    public class UpdateUserController : Controller
    {
        private readonly IUsersService usersService;

        public UpdateUserController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index()
        {

            return View("Pages/Index.cshtml");
        }



        public async Task<IActionResult> UpdateUser(User user)
        {
            try
            {
                var result = await usersService.Update(user.Id, user.Name, user.Role);

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
    }
}
