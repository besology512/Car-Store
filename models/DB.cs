using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Car_Store.models
{
    
    public class DB
    {
        SqlConnection con;

        public DB()
        {
            string conString = "Data Source=laptop-8njtos7o;Initial Catalog=TheCarStore;Integrated Security=True";
            con = new SqlConnection(conString);
        }
        
        public void insertUser(string Fname, string Lname, string pass, string date, string email)
        { //to return any data type
            string query = "insert into CLIENT values ('"+Fname+ "', '" + Lname + "', '" + pass + "', '" + date + "', '" + email + "')";
            object type;
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                type = sqlCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException) { con.Close();}
        }
    }
}
