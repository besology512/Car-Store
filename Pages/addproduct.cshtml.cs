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
        public void OnGet()
        {
            
        }
        public void OnPost()
        {
            database.insert_product(p.Category, p.branchId,p.quantity,p.brand,p.price,1,p.Description);
        }

    }
}
