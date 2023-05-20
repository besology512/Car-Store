using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;


namespace Car_Store.Pages
{
    public class DisplayAllCarsModel : PageModel
    {
        private readonly DB DATABASE;

        [BindProperty]
        public DataTable dtable { get; set; }

        [BindProperty]
        public DataTable statisticsTable { get; set; }
        public DisplayAllCarsModel(DB Db)
        {
            DATABASE = Db;
        }
        public void OnGet()
        {
            
            dtable = (DataTable)DATABASE.get_all_cars();
            statisticsTable = (DataTable)DATABASE.get_car_statistics();
        }
        public IActionResult OnPostDelete(int vehcId)
        {
            DATABASE.Delete_Car_by_admin(vehcId);
            return RedirectToPage("/DisplayAllCars");
        }
        
    }
}
