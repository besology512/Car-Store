using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;

namespace Car_Store.Pages
{
    public class AdminAddCarModel : PageModel
    {
        private readonly DB DATABASE;

        public String Alertmsg { get; set; }
        [BindProperty]
        public int CID { get; set; }

        [BindProperty]
        public string MyColor { get; set; }

        [BindProperty]
        public vehicle ParentV { get; set; }

        [BindProperty]
        public used_vehicle u_Vehicle { get; set; }

        public AdminAddCarModel(DB My_DB)
        {
            DATABASE = My_DB;   
        }
        public void OnGet()
        {
            Alertmsg = "";
        }
        public void OnPost(string Brand,  string Model, string Fuel, string CarDesc, int Year, int CarClass, string Cstyle, string GEAR, int CC, int Price, IFormFile carImg , int Quantity) 
        {
            if (carImg != null && carImg.Length > 0)
            {
                int carid = DATABASE.getTopVehicleId() + 1;

                string fileName = CarDesc.Replace(" ", "-") + "-" + carid.ToString() + ".jpg"; // we should inject something unique here like id

                string imagePath = Path.Combine("wwwroot", "images", fileName);

                // Save the uploaded image to the specified path
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    carImg.CopyTo(fileStream);
                }

                string finalPath = imagePath.Replace("wwwroot", "");

                
                CID = (int)HttpContext.Session.GetInt32("User_ID");

                DATABASE.admin_add_car(Brand, CC, MyColor, Year, GEAR, Cstyle, Price, CarClass, CarDesc, Model, Fuel, finalPath, Quantity);
                
            }

        }
    }
}
