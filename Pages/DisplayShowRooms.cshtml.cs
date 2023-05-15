using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Data;

namespace Car_Store.Pages
{
    public class DisplayShowRoomsModel : PageModel
    {
        public ShowRoom Room { get; set; }
        public DataTable DT { get; set; }
        public IActionResult OnGet()
        {
            Room = new ShowRoom();
            DT = (DataTable)Room.getAll();


            if (HttpContext.Session.GetInt32("User_Type") != 1)
            {
                return RedirectToPage("/index");
            }
            return Page();
           
        }
        public void OnPostDelete() {
            Console.WriteLine("Ahmed");
        }
    }
}