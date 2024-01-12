using ConqueringSpace.Interfaces;
using ConqueringSpace.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConqueringSpace.Pages
{
    public class AdminModel : PageModel
    {
        public static User userConnected { get; set; }
        private readonly ILogger<IndexModel> _logger;


        public AdminModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }



        public async Task OnGet()
        {
            if (userConnected != null)
            {
                ViewData["ConnectedUser"] = userConnected;
            }
        }
    
    }
}
