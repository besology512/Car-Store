using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Data;

namespace Car_Store.Pages
{
    public class profileModel : PageModel
    {
        [BindProperty]
        public int ID { get; set; }

        private readonly DB database;

        [BindProperty]
        public DataTable dt { get; set; }

        public profileModel( DB db)
        {
            database = db;
        }
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetInt32("User_Type") != 0)
            {
                return RedirectToPage("/index");
            }
            ID = (int)HttpContext.Session.GetInt32("User_ID");
            dt = (DataTable)database.getUser(ID);
            return Page();

        }
        public IActionResult OnPost(string Fname , string Lname , string Email , string Phone,string Pass) {

            ID = (int)HttpContext.Session.GetInt32("User_ID");
            database.edit_client_info(ID, Fname , Lname , Phone,Email, Pass);
            return RedirectToPage("/profile");
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");

        }
    }
}
