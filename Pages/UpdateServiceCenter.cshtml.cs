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
            myCenter.Name = (string)DT.Rows[0][1];
            myCenter.Address = (string)DT.Rows[0][2];
            myCenter.Services = (string)DT.Rows[0][3];
            myCenter.latitude = (decimal)DT.Rows[0][4];
            myCenter.longitude = (decimal)DT.Rows[0][5];
            myCenter.stars = (int)DT.Rows[0][6];
            return Page();

        }

        public IActionResult OnPost(string name, string address, string services, decimal latitude, decimal longitude, int stars)
        {
            myCenter = new ServicesCenters();
            myCenter.ID = ID;
            myCenter.Name = name;
            myCenter.Address = address;
            myCenter.Services = services;
            myCenter.latitude = latitude;
            myCenter.longitude = longitude;
            myCenter.stars = stars;
            myCenter.update(ID);
            return RedirectToPage("/index");

        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }
    }
}
