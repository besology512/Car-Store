using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Car_Store.models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Car_Store.Pages
{
    public class Add_CarModel : PageModel
    {
        private readonly DB DATABASE;



        [BindProperty]
        public string MyColor { get; set; }
        [BindProperty]
        vehicle ParentV { get; set; }
        [BindProperty]
        used_vehicle u_Vehicle { get; set; }
        public Add_CarModel(DB My_DB)
        {

            DATABASE = My_DB;

        }
        public void OnGet()
        {
        }

        public void OnPost(string brand, int year, int KM, int CARCLASS, string Cstyle, string GEAR, int CC, int pr)
        {

            DATABASE.insert_vechile(brand, CC, MyColor, year, GEAR, Cstyle, pr, KM, CARCLASS);
        }

    }
}
