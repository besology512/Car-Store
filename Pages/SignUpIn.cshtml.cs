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
        public void OnPost(string fname, string Lname, string phoneNumber, string date, string password, string Email)
        {
            Customer.fname = fname;
            Customer.Lname = Lname;
            Customer.phoneNumber = phoneNumber;
            Customer.date = date;
            Customer.password = password;
            Customer.Email = Email;        
            Customer.insert();
        }
    }
}
