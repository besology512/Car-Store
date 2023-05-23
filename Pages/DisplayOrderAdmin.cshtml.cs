using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Data;

namespace Car_Store.Pages
{
    public class DisplayOrderAdminModel : PageModel
    {
        private readonly DB My_DB;

        [BindProperty]
        public DataTable dt { get; set; }

        [BindProperty]
        public int ordersNum { get; set; }
        public DisplayOrderAdminModel(DB db)
        {
            My_DB = db;
        }
        public void OnGet()
        {
            dt = (DataTable)My_DB.get_all_orders_admin();
            ordersNum = My_DB.get_num_of_order();
        }
        public IActionResult OnPost(string OrderStatus , int OrderId)
        {
            My_DB.update_order_status(OrderStatus, OrderId);
            return RedirectToPage("/DisplayOrderAdmin");
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }
    }
}
