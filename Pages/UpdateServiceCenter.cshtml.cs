using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Car_Store.Pages
{
    public class UpdateServiceCenterModel : PageModel
    {
        public DB db { get; set; }
        public ServicesCenters myCenter { get; set; }
        [BindProperty]
        public int ID { get; set; }

        public DataTable DT { get; set; }
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetInt32("User_Type") != 1)
            {
                return RedirectToPage("/index");
            }
            DT = new DataTable();

            myCenter = new ServicesCenters();

            myCenter.ID = id;
            
            DT = (DataTable)myCenter.getTuple();
            myCenter.Name = (string)DT.Rows[0]["Name"];
            myCenter.Address = (string)DT.Rows[0]["Address"];
            myCenter.Services = (string)DT.Rows[0]["Services"];
            myCenter.PhoneNumber = (int)DT.Rows[0]["PhoneNumber"];
            myCenter.latitude = (decimal)DT.Rows[0]["latitude"];
            myCenter.longitude = (decimal)DT.Rows[0]["longitude"];
            myCenter.stars = (int)DT.Rows[0]["stars"];
            return Page();

        }

        public IActionResult OnPost(string name, string address, string services, decimal latitude, decimal longitude, int stars, int phone)
        {
            myCenter = new ServicesCenters();
            myCenter.ID = ID;
            myCenter.Name = name;
            myCenter.Address = address;
            myCenter.Services = services;
            myCenter.latitude = latitude;
            myCenter.longitude = longitude;
            myCenter.stars = stars;
            myCenter.PhoneNumber = phone;
            myCenter.update(ID);
            return RedirectToPage("/ServiceCenters");

        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }
    }
}
