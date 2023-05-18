using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Car_Store.Pages.Shared
{
    public class AddServicesCentersModel : PageModel
    {

        public ServicesCenters myCenter { get; set; }
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetInt32("User_Type") != 1)
            {
                return RedirectToPage("/index");
            }
            return Page();
        }


        public void OnPost(int id, string name, string address, string services, decimal latitude, decimal longitude, int stars)
        {
            myCenter = new ServicesCenters();
            myCenter.ID = id;
            myCenter.Name = name;
            myCenter.Address = address; 
            myCenter.Services = services;   
            myCenter.latitude = latitude;
            myCenter.longitude = longitude; 
            myCenter.stars = stars;
            myCenter.insert();

        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }
    }
}
