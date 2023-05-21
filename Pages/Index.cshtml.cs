using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Data;
using System;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Car_Store.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DB DB;

        [BindProperty]
        public DataTable dt { get; set; }
        [BindProperty]
        public int iteratoe { get; set; }

        [BindProperty]
        public int Landing_random { get; set; }
        [BindProperty]
        public WishCart cartWish { get; set; }



        public IndexModel(ILogger<IndexModel> logger,DB db)
        {
            _logger = logger;
            DB = db;
        }
        public void OnGet()
        {
            Random num = new Random();
            Landing_random = num.Next(0,DB.get_all_cars_count());
            dt = (DataTable)DB.get_all_cars();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }
        public IActionResult OnPostAddCarToCart(int PID)
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
            cartWish.typepv = 1;
            cartWish.typecw = 0;
            cartWish.insert();
            return RedirectToPage("/CartPage");
        }
        public IActionResult OnPostAddCarToWishList(int PID)
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
            cartWish.typepv = 1;
            cartWish.typecw = 1;
            cartWish.insert();
            return RedirectToPage("/Index");
        }
    }
}