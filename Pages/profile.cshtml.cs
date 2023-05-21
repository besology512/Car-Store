using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace Car_Store.Pages
{
    public class profileModel : PageModel
    {
        [BindProperty]
        public int ID { get; set; }

        private readonly DB database;

        [BindProperty]
        public DataTable dt { get; set; }

        public profileModel( DB db)
        {
            database = db;
        }
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetInt32("User_Type") != 0)
            {
                return RedirectToPage("/index");
            }
            ID = (int)HttpContext.Session.GetInt32("User_ID");
            dt = (DataTable)database.getUser(ID);
            return Page();

        }
        public IActionResult OnPost(string Fname , string Lname , string Email , string Phone,string Pass, IFormFile PImage) {
            string finalPath = Request.Form["PImage"];
            ID = (int)HttpContext.Session.GetInt32("User_ID");
            if (PImage != null && PImage.Length > 0)
            {
                
                Random rnum = new Random();
                int num = rnum.Next();
                string fileName = Fname.Replace(" ", "-") + "-" + ID.ToString()+num.ToString() + ".jpg"; // we should inject something unique here like id

                string imagePath = Path.Combine("wwwroot", "images", fileName);

                // Save the uploaded image to the specified path
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    PImage.CopyTo(fileStream);
                }

                finalPath = imagePath.Replace("wwwroot", "");
            }

                
            
            database.edit_client_info(ID, Fname , Lname , Phone,Email, Pass,finalPath);
            return RedirectToPage("/profile");
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");

        }
    }
}
