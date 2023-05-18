using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Data;

namespace Car_Store.Pages
{
    public class CutomerPostedCarModel : PageModel
    {

        private readonly DB Databse;
        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public DataTable dtable { get; set; }

        public CutomerPostedCarModel( DB Db)
        {
            Databse = Db;
        }
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetInt32("User_Type") != 0)
            {
                return RedirectToPage("/index");
            }

            Id = (int)HttpContext.Session.GetInt32("User_ID");

            dtable = (DataTable)Databse.GetPostedCar(Id);
                                       
            return Page();
        }
        public IActionResult OnPostDelete(int vehcId) {
            Id = (int)HttpContext.Session.GetInt32("User_ID");
            Databse.delet_CLIENT_POST(vehcId, Id);
            return RedirectToPage("/CutomerPostedCar");
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }

    }
}
