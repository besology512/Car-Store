using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System;
namespace Car_Store.models
{
    
    public class DB
    {
        SqlConnection con;

        public DB()
        {
            string conString = "Data Source=LAPTOP-8NJTOS7O;Initial Catalog=THECARSTORE;Integrated Security=True";
            con = new SqlConnection(conString);
        }
        
        public void insertUser(string Fname, string Lname, string pass, string date, string email)
        { //to return any data type
            string query = "insert into CLIENT values ('"+UserName+"','"+Fname+ "', '" + Lname + "', '" + pass + "', '" + date + "', '" + email + "', 0)";
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
