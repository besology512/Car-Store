using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Car_Store.models
{
    public class Client
    {
        private DB db = new DB();

        [Required]
        public int ClientID;
        [Required, MaxLength(100)]
        public string Client_Username { get; set; }
        [Required, MaxLength(20)]
        public string Client_FName;
        [Required, MaxLength(20)]
        public string Client_LName;
        [Required, MaxLength(30)]
        public string pass;
        [Required, MaxLength(20)]
        public string bdate; //should have Date type
        [Required, MaxLength(100), EmailAddress]
        public string Mail;
        [Required]
        public string phoneNumber;
        public int UserType { get; set; }



        public object getAll()
        {
            return db.ReadAll("CLIENT");
        }

        public void delete(int ID)
        {
            db.deletetuple("CLIENT", ID, "ClientID");
        }

        public void insert()
        {
            db.insertUser(this.Client_FName, this.Client_LName, this.pass, this.bdate, this.Mail, this.Client_Username);
        }

        public string getPasswordCl()
        {
            return (string)db.getPasswordClient(Client_Username);
        }
        public string getPasswordEmp()
        {
            return (string)db.getPasswordEmployee(Client_Username);
        }

        public DataTable getRow(string Name, string tablename, string pKey)
        {
            return (DataTable)db.ReadTuple(Name, tablename, pKey);
        }


    }
}
