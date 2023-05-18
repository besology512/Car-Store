using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Car_Store.Pages
{
    public class DeleteRoomModel : PageModel
    {

        public ShowRoom room { get; set; }
        
        public IActionResult OnGet(string name)
        {
            room = new ShowRoom();
            room.Name = name;


            if (HttpContext.Session.GetInt32("User_Type") != 1)
            {
                return RedirectToPage("/index");
            }
            return Page();

        }
        public IActionResult OnPostYes(string previousName)
        {
            room = new ShowRoom();
            room.delete(previousName);
            return RedirectToPage("/DisplayShowRooms");
        }
        public IActionResult OnPostNo(string previousName)
        {
            return RedirectToPage("/DisplayShowRooms");
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }
    }
}
