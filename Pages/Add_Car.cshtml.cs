using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Car_Store.models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using static System.Net.Mime.MediaTypeNames;

namespace Car_Store.Pages
{
    public class Add_CarModel : PageModel
    {
        private readonly DB DATABASE;

        public String  Alertmsg { get; set; }
        [BindProperty]
        public int CID { get; set; }

        [BindProperty]
        public string MyColor { get; set; }
        
        [BindProperty]
        public vehicle ParentV { get; set; }
        
        [BindProperty]
        public used_vehicle u_Vehicle { get; set; }
        public Add_CarModel(DB My_DB)
        {

            DATABASE = My_DB;

        }

        [BindProperty]
        public IFormFile carImg { get; set; }


        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetInt32("User_Type") != 1 && HttpContext.Session.GetInt32("User_Type") != 0)
            {
                return RedirectToPage("/index");
            }
            //DATABASE.del_temp();
            
            Alertmsg = "";
            //DATABASE.del_temp();
            return Page();
            
            

        }

        public void OnPost(string Brand,string City,string Model,string Fuel, string CarDesc, int Year, int KM, int CarClass, string Cstyle, string GEAR, int CC, int Price, IFormFile carImg)
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

                DATABASE.insert_vechile(Brand, CC, MyColor, Year, GEAR, Cstyle, Price, KM, CarClass, CarDesc, Model, Fuel, City, finalPath);
                
                CID = (int)HttpContext.Session.GetInt32("User_ID");
                
                DATABASE.insert_to_pendingposts(CID, carid);

            }

            Alertmsg = "Post is pending now it will approved by admin soon";
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }

    }
}
