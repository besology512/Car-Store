using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Car_Store.Pages
{
    public class ProductPageModel : PageModel
    {
        [BindProperty]
        public DataTable DT_N { get; set; }
        [BindProperty]

        public DataTable DT_U { get; set; }

        [BindProperty] 
        public vehicle myCar { get; set; }
        [BindProperty]
        public DB db { get; set; }

        public void OnGet(int id)
        {
            db = new DB();

            DT_N = (DataTable) db.getNewCar(id);
            DT_U = (DataTable)db.getUsedCar(id);
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }
    }
}
