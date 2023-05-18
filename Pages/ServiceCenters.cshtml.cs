using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Car_Store.Pages
{
    public class ServiceCentersModel : PageModel
    {
        public ServicesCenters myCenter { get; set; }
        public DataTable DT { get; set; }
        public string mapsLink { get; set; }
        public void OnGet()
        {
            myCenter = new ServicesCenters();
            DT = (DataTable)myCenter.getAll();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }
    }
}
