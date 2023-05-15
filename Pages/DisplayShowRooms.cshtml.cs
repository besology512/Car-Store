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
        public void OnGet()
        {
            Room = new ShowRoom();
            DT = (DataTable)Room.getAll();
        }
        public void OnPostDelete() {
            Console.WriteLine("Ahmed");
        }
    }
}