using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Data;

namespace Car_Store.Pages
{
    public class DisplayClientsModel : PageModel
    {
        public Client client { get; set; }
        public DataTable DT { get; set; } 
        public IActionResult OnGet()
        {
            client = new Client();
            DT = (DataTable)client.getAll();


            if (HttpContext.Session.GetInt32("User_Type") != 1)
            {
                return RedirectToPage("/index");
            }
            return Page();

        }
    }
}
