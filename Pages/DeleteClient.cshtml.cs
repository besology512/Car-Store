using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;


namespace Car_Store.Pages.Shared
{
    public class DeleteClientModel : PageModel
    {
        public Client client { get; set; }
        public void OnGet(int ID)
        {
            client = new Client();
            client.ClientID = ID;
        }

        public IActionResult OnPostYes(int ID)
        {
            client = new Client();
            client.delete(ID);
            return RedirectToPage("/DisplayClients");
        }
        public IActionResult OnPostNo(int ID)
        {
            return RedirectToPage("/DisplayClients");
        }


    }
}
