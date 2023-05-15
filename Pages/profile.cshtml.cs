using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Car_Store.Pages
{
    public class profileModel : PageModel
    {
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetInt32("User_Type") != 0)
            {
                return RedirectToPage("/index");
            }
            return Page();

        }
    }
}
