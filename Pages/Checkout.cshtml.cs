using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Car_Store.Pages
{
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        public WishCart wc { get; set; }

        public DataTable dtCarUsed { get; set; }
        public DataTable dtCarNew { get; set; }
        [BindProperty]
        public string Name { get; set; }     
        [BindProperty]
        public string City { get; set; }     
        [BindProperty]
        public string Street { get; set; }     
        [BindProperty]
        public string Building { get; set; }     
        [BindProperty]
        public string House { get; set; }     
        [BindProperty]
        public string PhoneNumber { get; set; }     
        [BindProperty]
        public List<int> CarList { get; set; } 
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetInt32("User_Type") != 0)
            {
                return RedirectToPage("/index");
            }
            wc = new WishCart();
            wc.CId = (int)HttpContext.Session.GetInt32("User_ID");
            dtCarUsed = wc.getCartUsed();
            dtCarNew = wc.getCartNew();
            return Page();
        }
        public IActionResult OnPostCheckout(string name, string city, string street, string building, string house, string phoneNumber) {
            //getting the ids of the cars this user have
            wc = new WishCart();
            wc.CId = (int)HttpContext.Session.GetInt32("User_ID");
            dtCarUsed = wc.getCartUsed();
            dtCarNew = wc.getCartNew();
            Name = name;
            City= city;
            Street= street;
            Building= building;
            House= house;
            PhoneNumber= phoneNumber;
            //adding the cars
            if (dtCarNew != null)
            {
                for (int i = 0; i < dtCarNew.Rows.Count; i++)
                {//insert into orderItems values(1, 40);
                    wc.addVehicleOrdered((int)dtCarNew.Rows[i][6]);
                    wc.minusone((int)dtCarNew.Rows[i][6]);
                }
            }
            if (dtCarUsed != null)
            {
                for (int i = 0; i < dtCarUsed.Rows.Count; i++)
                {
                    wc.addVehicleOrdered((int)dtCarUsed.Rows[i][6]);
                    wc.changeVisibility((int)dtCarUsed.Rows[i][6]);
                }
            }
            wc.addToOrders(city, street, building, house);
            return RedirectToPage("/Index");
        }
    }
}
