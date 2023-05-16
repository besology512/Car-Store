using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Data;
namespace Car_Store.Pages
{
    public class addproductModel : PageModel
    {
        private readonly DB database;
        [BindProperty]
        public product p { get; set; }
        public addproductModel(DB db)
        {
            database = db;
        }
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetInt32("User_Type") != 1)
            {
                return RedirectToPage("/index");
            }
            return Page();
        }
        public void OnPost(string carimage,string ptitle,string pcat,int branchid,int quantity,string brand,int price,int pId)
        {
            database.insert_product(pcat, branchid, quantity,brand,price, 1,ptitle,pId);
        }

    }
}
