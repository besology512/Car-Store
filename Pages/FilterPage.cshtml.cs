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
        public WishCart cartWish { get; set; }
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
            if (CheckedBrands.Count != 0 && CheckedColors.Count != 0)
            {
                PageCars = new List<vehicle>();
                foreach (string brand in CheckedBrands)
                {
                    foreach (string color in CheckedColors)
                    {
                        Console.WriteLine("I'm here with " + color + brand);
                        PageCars = PageCars.Concat(db.GetVehicles(Brand: brand, color: color)).ToList();
                        Console.WriteLine(PageCars.Count);
                    }
                }
                return;
            }
            else if (CheckedBrands.Count != 0 && CheckedColors.Count == 0)
            {
                PageCars = new List<vehicle>();
                foreach (string brand in CheckedBrands)
                {
                    Console.WriteLine("I'm here with " + brand);
                    PageCars = PageCars.Concat(db.GetVehicles(Brand: brand)).ToList();
                    Console.WriteLine(PageCars.Count);
                }
            }
            else if (CheckedColors.Count != 0 && CheckedBrands.Count == 0)
            {
                PageCars = new List<vehicle>();
                foreach (string color in CheckedColors)
                {
                    Console.WriteLine("the color I have is:" + color);
                    PageCars = PageCars.Concat(db.GetVehicles(color: color)).ToList();
                }
            }
            else
            {
                PageCars = db.GetVehicles();
            }
        }

        public void OnPostFilter()
        {
            Brands = db.GetAvailableFilters("Brands");
            gearing = db.GetAvailableFilters("gearing");
            MinMax = db.GetAvailableFilters("MinMax");
            Colors = db.GetAvailableFilters("Colors");
            status = db.GetAvailableFilters("status");
            for (int i = 0; i < Brands.Rows.Count; i++)
            {
                bool isChecked = Request.Form["brand_" + i] == "on";
                if (isChecked)
                {
                    CheckedBrands.Add(Brands.Rows[i][0].ToString());
                }
            }
            for (int i = 0; i < Colors.Rows.Count; i++)
            {
                bool isChecked = Request.Form["color_" + i] == "on";
                if (isChecked)
                {
                    CheckedColors.Add(Colors.Rows[i][0].ToString());
                }
            }
            OnGet();
            /*            HttpContext.Session.Set<List<string>>("CheckedBrands", CheckedBrands);*/
            /*return RedirectToAction("FilterPage");*/
        }
        public IActionResult OnPostAddCarToCart(int PID)
        {
            if (HttpContext.Session.GetInt32("User_ID") == null)
            {
                return RedirectToPage("/SignUpIn");
            }
            else
            {
                cartWish.CId = (int)HttpContext.Session.GetInt32("User_ID");
            }
            cartWish.PId = PID;
            cartWish.typepv = 1;
            cartWish.typecw = 0;
            cartWish.insert();
            return RedirectToPage("/FilterPage");
        }
        public IActionResult OnPostAddCarToWishList(int PID)
        {
            if (HttpContext.Session.GetInt32("User_ID") == null)
            {
                return RedirectToPage("/SignUpIn");
            }
            else
            {
                cartWish.CId = (int)HttpContext.Session.GetInt32("User_ID");
            }
            cartWish.PId = PID;
            cartWish.typepv = 1;
            cartWish.typecw = 1;
            cartWish.insert();
            return RedirectToPage("/FilterPage");
        }
        public IActionResult OnPostAddProductToWishList(int PID)
        {
            if (HttpContext.Session.GetInt32("User_ID") == null)
            {
                return RedirectToPage("/SignUpIn");
            }
            else
            {
                cartWish.CId = (int)HttpContext.Session.GetInt32("User_ID");
            }
            cartWish.PId = PID;
            cartWish.typepv = 0;
            cartWish.typecw = 1;
            cartWish.insert();
            return RedirectToPage("/FilterPage");
        }
        public IActionResult OnPostAddProductToCart(int PID)
        {
            if (HttpContext.Session.GetInt32("User_ID") == null)
            {
                return RedirectToPage("/SignUpIn");
            }
            else
            {
                cartWish.CId = (int)HttpContext.Session.GetInt32("User_ID");
            }
            cartWish.PId = PID;
            cartWish.typepv = 0;
            cartWish.typecw = 0;
            cartWish.insert();
            return RedirectToPage("/FilterPage");
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }
    }
}
