using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Data;
using Microsoft.SqlServer.Server;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.Drawing;

namespace Car_Store.Pages
{
    public class FilterPageModel : PageModel
    {
        [BindProperty]
        public List<string> Seats { get; set; }
        [BindProperty] public DataTable NumberOfSeatsAvailable { get; set; }
        [BindProperty]
        public string status { get; set; }
        [BindProperty]
        public bool Auto { get; set; }
        [BindProperty]
        public bool Manual { get; set; }
        [BindProperty]
        public WishCart cartWish { get; set; }
        public List<string> CheckedBrands { get; set; }
        public DataTable Brands { get; set; }
        [BindProperty] public DataTable Colors { get; set; }
        [BindProperty] public int ConstMinPrice { get; set; }
        [BindProperty] public int ConstMaxPrice { get; set; }

        [BindProperty] public int MinPrice { get; set; }
        [BindProperty] public int MaxPrice { get; set; }
        [BindProperty] public DataTable gearing { get; set; }
        [BindProperty] public DataTable warranty_years { get; set; }
        [BindProperty] public DataTable warranty_kilometers { get; set; }
        [BindProperty] public List<string> CheckedColors { get; set; }
        [BindProperty] public List<string> checkedWarrantyYears { get; set; }
        [BindProperty] public List<string> checkedWarrantyKilometers { get; set; }
        [BindProperty] public int selectedMIN { get; set; }
        [BindProperty] public int selectedMAX { get; set; }
        [BindProperty]
        public List<vehicle> PageCars { get; set; }
        [BindProperty]
        public DB db { get; set; }
        public FilterPageModel(DB myDatabase)
        {
            PageCars = new List<vehicle>();
            this.db = myDatabase;
            Colors = new DataTable();
            gearing = new DataTable();
            Brands = new DataTable();
            warranty_kilometers = new DataTable();
            warranty_years = new DataTable();
            CheckedBrands = new List<string>();
            //WHEN I DELETE IT IT DOESN'T WORK
            CheckedColors = new List<string>();
            checkedWarrantyYears = new List<string>();
            checkedWarrantyKilometers = new List<string>();
            cartWish = new WishCart();
            MinPrice = (int)(db.GetAvailableFilters("Price").Rows[0][1]);
            MaxPrice = (int)(db.GetAvailableFilters("Price").Rows[0][0]);
            ConstMinPrice = MinPrice;
            ConstMaxPrice = MaxPrice;
            Auto = false;
            Manual = false;
            status = "";
            Seats = new List<string>();
            NumberOfSeatsAvailable = new DataTable();
        }


        public void OnGet()
        {
            NumberOfSeatsAvailable = db.GetAvailableFilters("Seats");
            Brands = db.GetAvailableFilters("Brands");
            gearing = db.GetAvailableFilters("gearing");
            Colors = db.GetAvailableFilters("Colors");
            warranty_years = db.GetAvailableFilters("warranty years");
            warranty_kilometers = db.GetAvailableFilters("warranty kilometers");
            if (CheckedBrands.Count != 0 && CheckedColors.Count != 0 && Auto!=Manual)
            {
                PageCars = new List<vehicle>();
                foreach (string brand in CheckedBrands)
                {
                    foreach (string color in CheckedColors)
                    {
                        
                        if (Auto)
                        {
                            PageCars = PageCars.Concat(db.GetVehicles(Seats,Brand: brand, color: color, minPrice: MinPrice, maxPrice: MaxPrice,gearing:"Automatic",status:status)).ToList();
                        }
                        else
                        {
                            PageCars = PageCars.Concat(db.GetVehicles(Seats, Brand: brand, color: color, minPrice: MinPrice, maxPrice: MaxPrice ,gearing: "manual", status: status)).ToList();
                        }
                    }
                }
                return;
            }

            if (CheckedBrands.Count != 0 && CheckedColors.Count != 0)
            {
                PageCars = new List<vehicle>();
                foreach (string brand in CheckedBrands)
                {
                    foreach (string color in CheckedColors)
                    {
                        PageCars = PageCars.Concat(db.GetVehicles(Seats, Brand: brand, color: color, minPrice: MinPrice, maxPrice: MaxPrice, status: status)).ToList();
                    }
                }
                return;
            }
            else if (CheckedBrands.Count != 0 && CheckedColors.Count == 0 && Auto!=Manual)
            {
                PageCars = new List<vehicle>();
                foreach (string brand in CheckedBrands)
                {
                    if (Auto)
                    {
                        PageCars = PageCars.Concat(db.GetVehicles(Seats, Brand: brand, minPrice: MinPrice, maxPrice: MaxPrice, gearing: "Automatic", status: status)).ToList();
                    }
                    else
                    {
                        PageCars = PageCars.Concat(db.GetVehicles(Seats, Brand: brand, minPrice: MinPrice, maxPrice: MaxPrice, gearing: "manual", status: status)).ToList();
                    }
                }
                return;
            }
            else if (CheckedBrands.Count != 0 && CheckedColors.Count == 0)
            {
                PageCars = new List<vehicle>();
                foreach (string brand in CheckedBrands)
                {
                    PageCars = PageCars.Concat(db.GetVehicles(Seats, Brand: brand, minPrice: MinPrice, maxPrice: MaxPrice, status: status)).ToList();
                }
                return;
            }
            else if (CheckedColors.Count != 0 && CheckedBrands.Count == 0 && Auto!=Manual)
            {
                PageCars = new List<vehicle>();
                foreach (string color in CheckedColors)
                {
                    if (Auto)
                    {
                        PageCars = PageCars.Concat(db.GetVehicles(Seats, color: color, minPrice: MinPrice, maxPrice: MaxPrice, gearing: "Automatic", status: status)).ToList();
                    }
                    else
                    {
                        PageCars = PageCars.Concat(db.GetVehicles(Seats, color: color, minPrice: MinPrice, maxPrice: MaxPrice, gearing: "manual", status: status)).ToList();
                    }
                }
                return;
            }
            else if (CheckedColors.Count != 0 && CheckedBrands.Count == 0)
            {
                PageCars = new List<vehicle>();
                foreach (string color in CheckedColors)
                {
                    PageCars = PageCars.Concat(db.GetVehicles(Seats, color: color, minPrice: MinPrice, maxPrice: MaxPrice, status: status)).ToList();
                }
                return;
            }
            else if (Auto != Manual)
            {
                if (Auto)
                {
                    PageCars = db.GetVehicles(Seats, minPrice: MinPrice, maxPrice: MaxPrice, gearing: "Automatic", status: status);
                }
                else
                {
                    PageCars = db.GetVehicles(Seats, minPrice: MinPrice, maxPrice: MaxPrice, gearing: "Manual", status: status);
                }
            }
            else if(selectedMAX != MaxPrice || selectedMIN != MinPrice)
            {
                PageCars = db.GetVehicles(Seats, minPrice: MinPrice,maxPrice:MaxPrice, status: status);
            }
            else
            {
                PageCars = db.GetVehicles(Seats);
                return;
            }
        }

        public void OnPostFilter( int min_price, int max_price)
        {
            Brands = db.GetAvailableFilters("Brands");
            gearing = db.GetAvailableFilters("gearing");
            Colors = db.GetAvailableFilters("Colors");
            NumberOfSeatsAvailable = db.GetAvailableFilters("seats");
            MinPrice = min_price; MaxPrice = max_price;
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

            Manual = Request.Form["Manual"] == "on";
            Auto = Request.Form["Auto"] == "on";
            if (Request.Form["New"] == "on" && !(Request.Form["Used"] == "on"))
            {
                status = "new";
            }
            else if (!(Request.Form["New"] == "on") && (Request.Form["Used"] == "on"))
            {
                status = "Used";
            }
            else { status  = ""; }
            Console.WriteLine(NumberOfSeatsAvailable.Rows.Count);

            for (int i = 0; i < NumberOfSeatsAvailable.Rows.Count; i++)
            {
                if (Request.Form["seat_" + NumberOfSeatsAvailable.Rows[i][0].ToString()] == "on")
                {
                    Console.WriteLine(NumberOfSeatsAvailable.Rows[i][0]);
                    Seats.Add(NumberOfSeatsAvailable.Rows[i][0].ToString());
                }
            }
            OnGet();
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
    }
}
