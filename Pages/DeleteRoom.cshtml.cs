using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Car_Store.Pages
{
    public class DeleteRoomModel : PageModel
    {

        public ShowRoom room { get; set; }
        
        public void OnGet(string name)
        {
            room = new ShowRoom();
            room.Name = name;
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
    }
}
