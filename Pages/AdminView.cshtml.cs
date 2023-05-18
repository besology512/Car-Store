using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Car_Store.Pages
{
    public class AdminViewModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("User_Type") != 1)
            {
                return RedirectToPage("/index");
            }
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }
    }
}
