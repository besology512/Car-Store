using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
namespace Car_Store.Pages
{
    public class FilterPageModel : PageModel
    {
        [BindProperty]
        public List<vehicle> PageCars { get; set; }
        [BindProperty]
        public DB db { get; set; }
        public FilterPageModel(DB myDatabase)
        {
            PageCars = new List<vehicle>();
            this.db = myDatabase;
        }
        public void OnGet()
        {
            PageCars = db.GetVehicles();
            Console.WriteLine(PageCars.Count); //it worked YAAAAAAAAAAAAAAAAAAAAAAAAAS
        }
        public void OnPost()
        {

        }
    }
}
