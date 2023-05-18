using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Car_Store.Pages
{
    public class AdminAddCarModel : PageModel
    {
        [BindProperty]
        public string MyColor { get; set; }
        public void OnGet()
        {
        }
    }
}
