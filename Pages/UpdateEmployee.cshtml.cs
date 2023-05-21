using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.Data;

namespace Car_Store.Pages
{
    public class UpdateEmployeeModel : PageModel
    {
        public DB db { get; set; }
        [BindProperty] 
        public DataTable DT { get; set; }
        [BindProperty]
        public DataTable dt2 { get; set; }
        public int id { get; set; }


        public IActionResult OnGet(string name)
        {


            if (HttpContext.Session.GetInt32("User_Type") != 1)
            {
                return RedirectToPage("/index");
            }
            db = new DB();
            DT = new DataTable();
            id = (int)HttpContext.Session.GetInt32("User_ID");
            DT = (DataTable)db.ReadTuple(id, "employee", "ID");
            dt2 = (DataTable)db.get_all_branches_num();

            return Page();
        }

        public IActionResult OnPost(string UserName, string Password, string Fname, string Mname, string Lname, string SSN, string Bdate, string Gender, int SuperID, string JobType, int Branch, int Salary, int UserType, int DepID)
        {
            id = (int)HttpContext.Session.GetInt32("User_ID");
            db = new DB();
            db.updateEmployee(id, UserName, Fname, Mname, Lname, Password, SSN, Bdate, Gender, Salary, SuperID, UserType, JobType, Branch, DepID);
            return RedirectToPage("/index");
        }



    }
}


