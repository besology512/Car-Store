using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;

namespace Car_Store.Pages
{
    public class FilterPageModel : PageModel
    {
        [BindProperty]
        public WishCart cartWish { get; set; } 
        public void OnGet()
        {
        }
        public void OnPost() { 
        
        }
        public void OnPostAddToCart(int PID, int CID, int typepv, int typecw) {
            cartWish.CId = 1; /*PID;*/
            cartWish.PId = 1; /*PID;*/
            cartWish.typepv = 1;/*type;*/
            cartWish.typecw = 1;/*type;*/
            cartWish.insert();
        }
    }
}
