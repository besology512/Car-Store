using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Car_Store.models
{
    public class customer
    {
        private DB db= new DB();
        [Required]
        public int clientId;
        [Required, MaxLength(20)]
        public string fname;
        [Required, MaxLength(20)]
        public string Lname;
        [Required, MaxLength(100), EmailAddress]
        public string Email;
        [Required]
        public string phoneNumber;
        [Required]
        public string date;
        [Required, NotNull]
        public string password;
        public void insert()
        {
            db.insertUser(this.fname, this.Lname, this.password, this.date, this.Email);
        }
    }
}
