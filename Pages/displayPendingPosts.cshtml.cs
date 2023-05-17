using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Data;

namespace Car_Store.Pages
{
    public class displayPendingPostsModel : PageModel
    {
        private readonly DB DATABASE;

        [BindProperty]
        public DataTable dtable { get; set; }

        public displayPendingPostsModel(DB db)
        {
            DATABASE = db;
        }

        public void OnGet()
        {
            dtable = (DataTable)DATABASE.get_pending_post();
        }
        public IActionResult OnPostAccept(int ClientId, int vehcId)
        {
            DATABASE.approve_car(vehcId, ClientId);
            return RedirectToPage("/displayPendingPosts");
        }
        public IActionResult OnPostDecline(int ClientId, int vehcId)
        {
            DATABASE.declinecar(vehcId, ClientId);
            return RedirectToPage("/displayPendingPosts");
        }
    }
}
