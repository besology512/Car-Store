using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Store.models;

namespace Car_Store.Pages
{
    public class SignUpIn : PageModel
    {
        [BindProperty]
        public customer Customer { get; set; }

        //public SignUpIn(customer customer)
        //{
        //    Customer = customer;
        //}

        public SignUpIn(customer Customer)
        {
            this.Customer = Customer;
        }
        

        public void OnGet()
        {
        }
        public IActionResult OnPostSignUp(string fname, string Lname, string phoneNumber, string date, string password, string Email, string UserName)
        {
            Customer.fname = fname;
            Customer.Lname = Lname;
            Customer.phoneNumber = phoneNumber;
            Customer.date = date;
            Customer.password = password;
            Customer.Email = Email;        
            Customer.UserName = UserName;
            Customer.insert();
            return RedirectToPage("/Index");
        }
        public IActionResult OnPostLogin(string UserName2, string Password2) { 
            Customer.UserName = UserName2;
            Customer.password = Password2;
            string passcl = Customer.getPasswordCl();
            string passEmp = Customer.getPasswordEmp();
            if (passcl == passEmp && passEmp == "notFound")
            {
                return Page();
            }
            else if (passcl == "notFound" && passEmp == Password2)
            {
                return RedirectToPage("/Add_Car");
                //go to employee
            }
            else if (passEmp == "notFound" && passcl == Password2)
            {
                return RedirectToPage("/CartPage");
                //go to client
            }
            else {
                return Page();
            }

        }

    }
}
