using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Xml.Linq;


namespace Car_Store.Pages
{
    public class AddShowRoomModel : PageModel
    {
        [BindProperty]
        public ShowRoom room { get; set; }


        public void OnGet()
        {
        }
        public void OnPost(string name, string city, string phoneNumber, string street) {
            room.Name = name;
            room.City = city;
            room.PhoneNumber = phoneNumber;
            room.Street = street;
            room.insert();
        }
    }
}
