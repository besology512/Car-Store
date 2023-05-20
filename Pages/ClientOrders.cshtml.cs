using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Car_Store.Pages
{
    public class ClientOrdersModel : PageModel
    {
        [BindProperty]
        private DB DB { get; set; }

        public DataTable DataTable { get; set; }
        public ClientOrdersModel(DB dB)
        {
            this.DB = dB;
        }
        public void OnGet()
        {
            DataTable = (DataTable)DB.getOrders((int)HttpContext.Session.GetInt32("User_ID"));
        }
    }
}
