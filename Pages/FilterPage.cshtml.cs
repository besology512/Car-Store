using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;



namespace Car_Store.Pages
{
    public class FilterPageModel : PageModel
    {
        [BindProperty]
        public WishCart cartWish { get; set; } 

        public List<vehicle> PageCars { get; set; }
        [BindProperty]
        public DB db { get; set; }
        public FilterPageModel(DB myDatabase)
        {
            PageCars = new List<vehicle>();
            this.db = myDatabase;
        }

        public void OnGet()
        {
            PageCars = db.GetVehicles();
        }

        public void OnPost() { 
        
        }
        public IActionResult OnPostAddCarToCart(int PID) {
            if (HttpContext.Session.GetInt32("User_ID") == null)
            {
                return RedirectToPage("/SignUpIn");
            }
            else
            {
                cartWish.CId = (int)HttpContext.Session.GetInt32("User_ID");
            }
            cartWish.PId = PID; 
            cartWish.typepv = 1;
            cartWish.typecw = 0;
            cartWish.insert();
            return RedirectToPage("/FilterPage");
        }
        public IActionResult OnPostAddCarToWishList(int PID)
        {
            if (HttpContext.Session.GetInt32("User_ID") == null)
            {
                return RedirectToPage("/SignUpIn");
            }
            else { 
                cartWish.CId = (int)HttpContext.Session.GetInt32("User_ID");
            }
            cartWish.PId = PID;
            cartWish.typepv = 1;
            cartWish.typecw = 1;
            cartWish.insert();
            return RedirectToPage("/FilterPage");
        }
        public IActionResult OnPostAddProductToWishList(int PID)
        {
            if (HttpContext.Session.GetInt32("User_ID") == null)
            {
                return RedirectToPage("/SignUpIn");
            }
            else
            {
                cartWish.CId = (int)HttpContext.Session.GetInt32("User_ID");
            }
            cartWish.PId = PID;
            cartWish.typepv = 0;
            cartWish.typecw = 1;
            cartWish.insert();
            return RedirectToPage("/FilterPage");
        }
        public IActionResult OnPostAddProductToCart(int PID)
        {
            if (HttpContext.Session.GetInt32("User_ID") == null)
            {
                return RedirectToPage("/SignUpIn");
            }
            else
            {
                cartWish.CId = (int)HttpContext.Session.GetInt32("User_ID");
            }
            cartWish.PId = PID;
            cartWish.typepv = 0;
            cartWish.typecw = 0;
            cartWish.insert();
            return RedirectToPage("/FilterPage");
        }
    }
}
