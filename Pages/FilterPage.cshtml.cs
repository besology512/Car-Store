using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Data;
using Microsoft.SqlServer.Server;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Car_Store.Pages
{
    public class FilterPageModel : PageModel
    {
        [BindProperty]
        public List<string> CheckedBrands { get; set; }
        public DataTable Brands { get; set; }
        [BindProperty] public DataTable Colors { get; set; }
        [BindProperty] public DataTable MinMax { get; set; }
        [BindProperty] public DataTable gearing { get; set; }
        [BindProperty] public DataTable warranty_years { get; set; }
        [BindProperty] public DataTable warranty_kilometers { get; set; }
        [BindProperty] public DataTable status { get; set; }
        [BindProperty] public List<string> CheckedColors { get; set; }
        [BindProperty] public List<string> checkedWarrantyYears { get; set; }
        [BindProperty] public List<string> checkedWarrantyKilometers { get; set; }
/*        [BindProperty]
        public List<string> CheckedBrands
        {
            get
            {
                List<string> list = HttpContext.Session.Get<List<string>>("CheckedBrands");
                if (list == null)
                {
                    list = new List<string>();
                    HttpContext.Session.Set<List<string>>("MyList", list);
                }
                return list;
            }
            set { }
        }*/


        public WishCart cartWish { get; set; }
        [BindProperty]
        public List<vehicle> PageCars { get; set; }
        [BindProperty]
        public DB db { get; set; }
        public FilterPageModel(DB myDatabase)
        {
            PageCars = new List<vehicle>();
            this.db = myDatabase;
            Colors = new DataTable();
            MinMax = new DataTable();
            gearing = new DataTable();
            Brands = new DataTable();
            status = new DataTable();
            warranty_kilometers = new DataTable();
            warranty_years = new DataTable();
            CheckedBrands = new List<string>();
            Console.WriteLine("construction");
            //WHEN I DELETE IT IT DOESN'T WORK
            CheckedColors = new List<string>();
            checkedWarrantyYears = new List<string>();
            checkedWarrantyKilometers = new List<string>();
            cartWish = new WishCart();
        }

/*        protected void FilterByBrand(object sender, EventArgs e)
        {
            // Create a list to store the checked brand names
            List<string> checkedBrands = new List<string>();

            // Iterate through the checkboxes to check the state of each one
            foreach (var control in brand_1.Controls)
            {
                if (control is CheckBox checkbox && checkbox.Checked)
                {
                    // If the checkbox is checked, add its brand name to the list
                    var brandName = checkbox.Parent.Controls[0].ToString();
                    checkedBrands.Add(brandName);
                }
            }
        }*/


        public void OnGet()
        {
            Brands = db.GetAvailableFilters("Brands");
            gearing = db.GetAvailableFilters("gearing");
            MinMax = db.GetAvailableFilters("MinMax");
            Colors = db.GetAvailableFilters("Colors");
            status = db.GetAvailableFilters("status");
            warranty_years = db.GetAvailableFilters("warranty years");
            warranty_kilometers = db.GetAvailableFilters("warranty kilometers");
            Console.WriteLine(CheckedBrands.Count);
            if (CheckedBrands.Count != 0)
            {
                PageCars = new List<vehicle>();
                foreach (string brand in CheckedBrands)
                {
                    Console.WriteLine("I'm here with "+brand);
                    PageCars = PageCars.Concat(db.GetVehicles(Brand:brand)).ToList();
                    Console.WriteLine(PageCars.Count);
                }
            }
            else
            {
                PageCars = db.GetVehicles();
            }
        }

    public void OnPostFilter() {
            Brands = db.GetAvailableFilters("Brands");
            Console.WriteLine("rows are: " + Brands.Rows.Count);
            for (int i = 0; i < Brands.Rows.Count; i++)
            {
                bool isChecked = Request.Form["brand_"+i] == "on";
                if (isChecked) {
                    CheckedBrands.Add(Brands.Rows[i][0].ToString());
                    Console.WriteLine(Brands.Rows[i][0].ToString()); 
                }
            }
            OnGet();
/*            HttpContext.Session.Set<List<string>>("CheckedBrands", CheckedBrands);*/
            /*return RedirectToAction("FilterPage");*/
        }
        public void OnPostAddToCart(int PID, int CID, int typepv, int typecw) {
            cartWish.CId = 1; /*PID;*/
            cartWish.PId = 1; /*PID;*/
            cartWish.typepv = 1;/*type;*/
            cartWish.typecw = 1;/*type;*/
            cartWish.insert();
            //the logic of the filter
        }
    }
}
