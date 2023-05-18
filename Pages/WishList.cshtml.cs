using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Car_Store.Pages
{
    public class WhishListModel : PageModel
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
            wc.CId = (int)HttpContext.Session.GetInt32("User_ID");
            dtCarUsed = wc.getWishUsed();
            dtCarNew = wc.getWishNew();
            return Page();
        }

        public IActionResult OnPostDelete(int pID) { 
            wc.CId = (int)HttpContext.Session.GetInt32("User_ID");
            wc.PId = pID;
            wc.delete("wishlist_vehicle", "vehichle_ID");
            return RedirectToPage("/WishList");

        }
        public IActionResult OnPostAddToCart(int pID) {
            wc.CId = (int)HttpContext.Session.GetInt32("User_ID");
            wc.PId = pID;
            wc.typepv = 1;
            wc.typecw = 0;
            wc.delete("wishlist_vehicle", "vehichle_ID");
            wc.insert();
            return RedirectToPage("/WishList");
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }
    }
}
