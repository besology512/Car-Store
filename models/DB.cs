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
            string conString = "Data Source=DESKTOP-KQT84LF\\MSSQLSERVER2023;Initial Catalog=THECARSTORE;Integrated Security=True";
            con = new SqlConnection(conString);
        }

        public void insert_vechile(string Brand,int CC,string Color,int year_Model,string Gearing,string B_style,int price ,int Km,int car_class)
        {
            string q = "INSERT INTO VEHICLE(Car_Status,Brand,CC_Rnage,Color,Year_Model,Gearing,Body_Style)" + 
                "VALUES('Used','" + Brand + "'," + CC + ",'" + Color + "'," + year_Model + ",'" + Gearing + "','" + B_style + "')";
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("yyyy-MM-dd");
            string q2 = "INSERT INTO USED_VEHICLE VALUES("+ 1 + "," + Km + "," + price + ",'" + formattedDate + "',"+car_class+")";
            excute_nonQuery(q);
            excute_nonQuery(q2);
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
        private object Readtable(string Q)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                dt.Load(cmd.ExecuteReader());
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                con.Close();
                return ex;
            }
        }

        private object getsinglevalue(string Q)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                object scalar = cmd.ExecuteScalar();
                con.Close();
                return scalar;
            }
            catch (Exception ex)
            {
                con.Close();
                return ex;
            }
        }

        private object excute_nonQuery(string Q)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Q, con);
                int num = cmd.ExecuteNonQuery();
                con.Close();
                return num;


            }
            catch (Exception ex)
            {
                con.Close();
                return ex;

            }
        }
    }
}
