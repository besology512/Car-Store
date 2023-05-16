using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Car_Store.Pages
{
    public class SignUpIn : PageModel
    {
        [BindProperty]
        public Client Customer { get; set; }

        //public SignUpIn(customer customer)
        //{
        //    Customer = customer;
        //}

        public DataTable dt { get; set; }

        public SignUpIn(Client Customer)
        {
            this.Customer = Customer;
        }




        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("User_Type") == 1 || HttpContext.Session.GetInt32("User_Type") == 0)
            {
                return RedirectToPage("/index");
            }
            return Page();

        }

        public IActionResult OnPostSignUp(string fname, string Lname, string phoneNumber, string date, string password, string Email, string UserName)
        {

            Customer.Client_FName = fname;
            Customer.Client_LName = Lname;
            Customer.phoneNumber = phoneNumber;
            Customer.bdate = date;
            Customer.pass = password;
            Customer.Mail = Email;        
            Customer.Client_Username = UserName;
            Customer.insert();
            Console.WriteLine("I signed up");
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostLogin(string UserName2, string Password2) { 
            Customer.Client_Username = UserName2;
            Customer.pass = Password2;
            string passcl = Customer.getPasswordCl();
            string passEmp = Customer.getPasswordEmp();

            if (passcl == passEmp && passEmp == "notFound")
            {
                return Page();
            }
            else if (passcl == "notFound" && passEmp == Password2)
            {
                dt = Customer.getRow(UserName2, "EMPLOYEE", "Emp_Username");
                HttpContext.Session.SetInt32("User_ID", (int)dt.Rows[0][0]);
                HttpContext.Session.SetInt32("User_Type", (int)dt.Rows[0][11]);

                Console.WriteLine(HttpContext.Session.GetInt32("User_Type"));
                return RedirectToPage("/Add_Car");
                //go to employee
            }
            else if (passEmp == "notFound" && passcl == Password2)
            {
                dt = Customer.getRow(UserName2, "CLIENT", "Client_Username");
                HttpContext.Session.SetInt32("User_ID", (int)dt.Rows[0][0]);
                HttpContext.Session.SetInt32("User_Type", (int)dt.Rows[0][9]);

                return RedirectToPage("/index");

            }
            else {
                return Page();
            }

        }

    }
}
