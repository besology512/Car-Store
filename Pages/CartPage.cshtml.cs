using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Security.Cryptography;

namespace Car_Store.Pages
{
    public class CartPageModel : PageModel
    {
        [BindProperty]
        public WishCart wc { get; set; }

        public DataTable dtCarUsed { get; set; }
        public DataTable dtCarNew { get; set; }
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetInt32("User_Type") != 0)
            {
                return RedirectToPage("/index");
            }
            wc = new WishCart();
            wc.CId= (int)HttpContext.Session.GetInt32("User_ID");
            dtCarUsed = wc.getCartUsed();
            dtCarNew = wc.getCartNew();
            return Page();
        }

        public IActionResult OnPostRemove(int pId) { 
            wc.CId = (int)HttpContext.Session.GetInt32("User_ID");
            wc.PId = pId;
            wc.delete("Cart_vehicle", "vehichle_ID");
            return RedirectToPage("/CartPage");
        }


        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }
    }
}
