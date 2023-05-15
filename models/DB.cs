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
            string conString = "Data Source=Bassam;Initial Catalog=THECARSTORE;Integrated Security=True";
            con = new SqlConnection(conString);
        }

        public void insertUser(string Fname, string Lname, string pass, string date, string email, string UserName)
        { //to return any data type
            string query = "insert into CLIENT values ('" + UserName + "','" + Fname + "', '" + Lname + "', '" + pass + "', '" + date + "', '" + email + "', 0)";
            object type;
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                type = sqlCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException) { con.Close(); }
        }
        public List<vehicle> GetVehicles(
            string car_status = "",
            string showroom = "",
            string Brand = "",
            int CC_Range = 0,
            string color = "",
            int year_model = 0,
            string gearing = "",
            string body_Style = "",
            string car_image = "")
        {
            string Q = "";
            if (showroom != "" && car_status != "" && Brand != "" && CC_Range != 0 && color != "" && year_model != 0 && gearing != "" && body_Style != "" && car_image != "")
            {
                Q = "select * from VEHICLE " +
                   "where showroom =  '" + showroom + "' " +
                   "and car_status = '" + car_status + "' " +
                   "and Brand = '" + Brand + "' " +
                   "and CC_Rnage = '" + CC_Range +
                   "and Color = '" + color + "' " +
                   "and Year_Model = '" + year_model +
                   "and Gearing = '" + gearing + "' " +
                   "and Body_Style = '" + body_Style + "' " +
                   "and iimage = '" + car_image + "' ";
            }
            else if (car_status != "" && Brand != "" && CC_Range != 0 && color != "" && year_model != 0 && gearing != "" && body_Style != "" && car_image != "")
            {
                Q = "select * from VEHICLE " +
                    "where car_status =  '" + car_status + "' " +
                    "and Brand = '" + Brand + "' " +
                    "and CC_Rnage = '" + CC_Range +
                    "and Color = '" + color + "' " +
                   "and Year_Model = '" + year_model +
                   "and Gearing = '" + gearing + "' " +
                   "and Body_Style = '" + body_Style + "' " +
                   "and iimage = '" + car_image + "' ";
            }
            else if (Brand != "" && CC_Range != 0 && color != "" && year_model != 0 && gearing != "" && body_Style != "" && car_image != "")
            {
                Q = "select * from VEHICLE " +
                    "where Brand =  '" + Brand + "' " +
                    "and CC_Rnage = '" + CC_Range +
                    "and Color = '" + color + "' " +
                    "and Year_Model = '" + year_model +
                   "and Gearing = '" + gearing + "' " +
                   "and Body_Style = '" + body_Style + "' " +
                   "and iimage = '" + car_image + "' ";
            }
            else if (CC_Range != 0 && color != "" && year_model != 0 && gearing != "" && body_Style != "" && car_image != "")
            {
                Q = "select * from VEHICLE " +
                    "where CC_Rnage =  '" + CC_Range +
                    "and Color = '" + color + "' " +
                    "and Year_Model = '" + year_model +
                   "and Gearing = '" + gearing + "' " +
                   "and Body_Style = '" + body_Style + "' " +
                   "and iimage = '" + car_image + "' ";
            }
            else if (color != "" && year_model != 0 && gearing != "" && body_Style != "" && car_image != "")
            {
                Q = "select * from VEHICLE " +
                    "where Color =  '" + color + "' " +
                    "and Year_Model = '" + year_model +
                   "and Gearing = '" + gearing + "' " +
                   "and Body_Style = '" + body_Style + "' " +
                   "and iimage = '" + car_image + "' ";
            }
            else if (year_model != 0 && gearing != "" && body_Style != "" && car_image != "")
            {
                Q = "select * from VEHICLE " +
                    "where Year_Model =  '" + year_model +
                   "and Gearing = '" + gearing + "' " +
                   "and Body_Style = '" + body_Style + "' " +
                   "and iimage = '" + car_image + "' ";
            }
            else if (gearing != "" && body_Style != "" && car_image != "")
            {
                Q = "select * from VEHICLE " +
                    "where Gearing =  '" + gearing + "' " +
                   "and Body_Style = '" + body_Style + "' " +
                   "and iimage = '" + car_image + "' ";
            }
            else if (body_Style != "" && car_image != "")
            {
                Q = "select * from VEHICLE " +
                    "where Body_Style =  '" + body_Style + "' " +
                   "and iimage = '" + car_image + "' ";
            }
            else if (car_image != "")
            {
                Q = "select * from VEHICLE " +
                    "where iimage =  '" + car_image + "' ";
            }
            else
            {
                Q = "select * from VEHICLE ";
            }

            DataTable table = (DataTable)readTable(Q);
            List<vehicle> returned = new List<vehicle>();
            foreach (DataRow row in table.Rows)
            {

                vehicle newVehicle = new vehicle(
                   car_status: row["Car_Status"] != null ? row["Car_Status"].ToString() : "",
                   showroom: row["SHOWROOM"] != null ? row["SHOWROOM"].ToString() : "",
                   Brand: row["Brand"] != null ? row["Brand"].ToString() : "",
                   CC_Range: row["CC_Rnage"] != null ? (int)row["CC_Rnage"] : 0,
                   color: row["Color"] != null ? row["Color"].ToString() : "",
                   year_model: row["Year_Model"] != null ? (int)row["Year_Model"] : 0,
                   Gearing: row["Gearing"] != null ? row["Gearing"].ToString() : "",
                   Body_Style: row["Body_Style"] != null ? row["Body_Style"].ToString() : "",
                   car_image: row["iimage"] is null ? row["iimage"].ToString() : ""
                   );
                returned.Add(newVehicle);
            }
            return returned;
        }
        private object readTable(string Q)

        public void insert_vechile(string Brand, int CC, string Color, int year_Model, string Gearing, string B_style, int price, int Km, int car_class)
        {
            string q = "INSERT INTO VEHICLE(Car_Status,Brand,CC_Rnage,Color,Year_Model,Gearing,Body_Style)" +
                "VALUES('Used','" + Brand + "'," + CC + ",'" + Color + "'," + year_Model + ",'" + Gearing + "','" + B_style + "')";
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("yyyy-MM-dd");
            string q2 = "INSERT INTO USED_VEHICLE VALUES(" + 1 + "," + Km + "," + price + ",'" + formattedDate + "'," + car_class + ")";
            excute_nonQuery(q);
            excute_nonQuery(q2);
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



        public void insertShowRoom(string Name, string City, string street, string phoneNumber)
        { //to return any data type
            string query = "insert into SHOWROOM values ('" + Name + "', '" + City + "', '" + street + "', '" + phoneNumber + "')";
            object type;
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                type = sqlCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException) { con.Close(); }
        }
        public object ReadAll(string tablename)
        { //to return any data type
            string query = "select * from " + tablename;
            return FunctionReaderExecute(query);
        }
        public object ReadTuple(string Name, string tablename, string pKey)
        { //to return any data type
            string query = "select * " + " from " + tablename + " where " + pKey + " = " + "'" + Name + "';";
            return FunctionReaderTupleExecute(query);
        }
        private object FunctionReaderExecute(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                dt.Load(sqlCommand.ExecuteReader());
                con.Close();
                return dt;
            }
            catch (SqlException ex) { con.Close(); return ex; }

        }

        private object FunctionReaderTupleExecute(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                dt.Load(sqlCommand.ExecuteReader());
                con.Close();
                return dt;
            }
            catch (SqlException ex) { con.Close(); return ex; }

        }
        public void updateTupleRoom(string PreviousName, string Name, string City, string street, string phoneNumber)
        {
            string query = "update SHOWROOM set Show_Room_Name = '" + Name + "', City = '" + City + "', STreet = '" + street + "', phoneNumber = '" + phoneNumber + "' where Show_Room_Name = '" + PreviousName + "';";
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException) { con.Close(); }
        }
        public void deletetuple(string tableName, string pKey, string pColumn)
        {
            string query = "delete from " + tableName + " where " + pColumn + " = '" + pKey + "';";
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex) { con.Close(); }
        }

        // overloading if the primarykey is integer
        public void deletetuple(string tableName, int pKey, string pColumn)
        {
            string query = "delete from " + tableName + " where " + pColumn + " =" + pKey + ";";
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex) { con.Close(); }
        }
        public object getPasswordClient(string UserName)
        { //to return any data type
            string query = "select pass from CLIENT where Client_Username = '" + UserName + "';";
            object pass;
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                pass = sqlCommand.ExecuteScalar();
                if (pass == null)
                {
                    con.Close();
                    return "notFound";
                }
                con.Close();
                return (string)pass;
            }
            catch (SqlException) { con.Close(); return "notFound"; }
        }

        public object getPasswordEmployee(string UserName)
        { //to return any data type
            string query = "select pass from EMPLOYEE where Emp_Username = '" + UserName + "';";
            object pass;
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                pass = sqlCommand.ExecuteScalar();
                if (pass == null)
                {
                    con.Close();
                    return "notFound";
                }
                con.Close();
                return (string)pass;
            }
            catch (SqlException) { con.Close(); return "notFound"; }
        }



    }
}

