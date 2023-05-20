using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using System.Data;

namespace Car_Store.Pages
{
    public class AddEmployeeModel : PageModel
    {
        private readonly DB My_DB;
        [BindProperty]
        
        public DataTable dt1 {  get; set; }
        [BindProperty]
        public DataTable dt2 {  get; set; }
        [BindProperty]
        public string message { get; set; }
        public AddEmployeeModel(DB db)
        {
            My_DB = db;
            
        }
        public void OnGet()
        {
            dt1 = (DataTable)My_DB.get_department_num();
            dt2 = (DataTable)My_DB.get_all_branches_num();

        }

        public void OnPost(string UserName,string Password,string Fname ,string Mname,string Lname,int empSSN, string Bdate,string Gender,int SuperID,string JobType,int Branch,int Salary,int UserType,int DepID) 
        { 
            My_DB.insert_emp(UserName,Fname,Mname,Lname,Password,empSSN,Bdate,Gender,Salary,SuperID,UserType,JobType,Branch, DepID);
        }
    }
}
