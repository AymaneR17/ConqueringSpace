using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication2.Pages
{
    public class IndexModel : PageModel
    {


        public string message = "test hello test";

        public void OnGet()
        {
            
            ViewData["Message"] = message;
        }
    }
}