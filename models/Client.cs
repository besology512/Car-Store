using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Car_Store.models
{
    public class Client
    {
        private DB db = new DB();

        [Required]
        public int ClientID;
        [Required, MaxLength(20)]
        public string Client_FName;
        [Required, MaxLength(20)]
        public string Client_LName;
        [Required, MaxLength(30)]
        public string pass;
        [Required, MaxLength(20)]
        public DateOnly bdate; //should have Date type
        [Required, MaxLength(100), EmailAddress]
        public string Mail;



        public object getAll()
        {
            return db.ReadAll("CLIENT");
        }

        public void delete(int ID)
        {
            db.deletetuple("CLIENT", ID, "ClientID");
        }



    }
}
