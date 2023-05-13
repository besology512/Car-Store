using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Data;
using System.Collections.Specialized;


namespace Car_Store.Pages
{
    public class UpdateRoomModel : PageModel
    {
        public string previousNamee { get; set; }
        public DataTable DT { get; set; }
        public ShowRoom room { get; set; }
        public ShowRoom newroom { get; set; }

        public void OnGet(string name)
        {
            room = new ShowRoom();
            room.Name = name;
            DT = (DataTable)room.getTuple(); 
        }
        public IActionResult OnPost(string name, string city, string phoneNumber, string street, string previousName)
        {
            previousNamee = previousName;
            newroom = new ShowRoom();
            newroom.Name = name;
            newroom.City = city;
            newroom.PhoneNumber = phoneNumber;
            newroom.Street = street;
            newroom.update(previousNamee);
            return RedirectToPage("/DisplayShowRooms");
        }
    }
}