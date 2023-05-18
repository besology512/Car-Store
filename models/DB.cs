using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Security.Cryptography;

namespace Car_Store.models
{

    public class DB
    {
        SqlConnection con;

        public DB()
        {
            string conString = "Data Source=DESKTOP-KDC2LT0;Initial Catalog=TRMBcar;Integrated Security=True";



            con = new SqlConnection(conString);
        }

        public string date1 { get; set; }
        public void insertUser(string Fname, string Lname, string pass, string phoneNumber, string date, string email, string UserName)
        { //to return any data type
            /*            string date1 = date.ToString();
            */

            string query = "insert into CLIENT values ('" + UserName + "','" + Fname + "', '" + Lname + "', null , '" + phoneNumber + "', '" + pass + "', '" + date + "', '" + email + "', 0)";
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



        public void insert_vechile(string Brand, int CC, string Color, int year_Model, string Gearing, string B_style, int price, int Km, int car_class, string cardes, string name, string fuel, string city)
        {
            string q = "INSERT INTO VEHICLE(Car_Status,Brand,CC_Rnage,Color,Year_Model,Gearing,Body_Style,CarDescription,name,Fuel,visibality)" +
            "VALUES('Used','" + Brand + "'," + CC + ",'" + Color + "'," + year_Model + ",'" + Gearing + "','" + B_style + "','" + cardes
            + "','" + name + "','" + fuel + "'," + 0 + ")";

            DateTime currentDate = DateTime.Today;

            string formattedDate = currentDate.ToString("yyyy-MM-dd");

            excute_nonQuery(q);

            int vecId = getTopVehicleId();

            string q2 = "INSERT INTO USED_VEHICLE VALUES(" + vecId + "," + Km + "," + price + ",'" + formattedDate + "'," + car_class + ",'" + city + "')";

            excute_nonQuery(q2);
        }

        public void insert_to_pendingposts(int clientID , int vehcId)
        {
            string q = "insert into PENDING_POSTS values( " + clientID + "," + vehcId + ")";
            excute_nonQuery(q);
        } 
        public int getTopVehicleId()
        {
            string q = "SELECT  TOP 1 Vehicle_No FROM VEHICLE ORDER BY Vehicle_No DESC";
            return (int)getsinglevalue(q);
        }
        public void insert_CLIENT_POSTS(int clientid, int vehicleId)

        {
            string q = "INSERT INTO Client_Posts VALUES(" + clientid + " , " + vehicleId + ")";
            excute_nonQuery(q);
        }

        public void delet_CLIENT_POST(int vecId, int ClieTnID)
        {
            string q = "delete from Client_Posts where ClientId =  " + ClieTnID + " AND " + "VehcileId = " + vecId;
            excute_nonQuery(q);
        }

        public void insert_product(string category, int branchId, int qunatity, string brand, int price, int status, string description, int pid)
        {
            string q = "INSERT INTO PRODUCT (CATEGORY,BranchNo,Quantity,brand,Price,Statuss,Product_title,ProductID) VALUES('" + category + "'," + branchId + "," + qunatity + ",'" + brand + "'," + price + "," + status + ",'" + description + "'," + pid + ")";
            excute_nonQuery(q);
        }

        public void approve_car(int carid,int clientID)
        {
            insert_CLIENT_POSTS(clientID, carid);
            string q1 = "update VEHICLE set visibality = 1  " +
                "where VEHICLE.Vehicle_No = " + carid;
            excute_nonQuery(q1);
            string q2 = "delete from PENDING_POSTS where PENDING_POSTS.CLIENT_ID = " + clientID +
                " AND PENDING_POSTs.VEHCILE_ID = " + carid;
            excute_nonQuery(q2);
        }

        public void declinecar(int carid, int clientID)
        {
            string q1 = "delete from PENDING_POSTS where PENDING_POSTS.CLIENT_ID = " + clientID +
                " AND PENDING_POSTs.VEHCILE_ID = " + carid;
            excute_nonQuery(q1);
            string q2 = "delete from VEHICLE where VEHICLE.Vehicle_No = " + carid;
            excute_nonQuery(q2);
        }

        public object get_pending_post()
        {
            string q = "select  VEHICLE.CarDescription, USED_VEHICLE.Posting_Date, USED_VEHICLE.Price, VEHICLE.CC_Rnage, PENDING_POSTS.VEHCILE_ID, CLIENT.Client_Username , PENDING_POSTS.CLIENT_ID " +
                "from (VEHICLE JOIN USED_VEHICLE ON VEHICLE.Vehicle_No = USED_VEHICLE.Vehicle_ID) " +
                "JOIN PENDING_POSTS  ON VEHICLE.Vehicle_No = PENDING_POSTS.VEHCILE_ID JOIN CLIENT ON CLIENT.ClientID = PENDING_POSTS.CLIENT_ID";
            return Readtable(q);
        }

        public void edit_client_info(int clientID,string Fname , string Lname , string phone , string mail,string pass)
        {
            string q = "update CLIENT set Client_FName = '" + Fname + "'" +
                " , Client_LName = '" + Lname + "'" + " , Client_phone = '" + phone + "'"
                + " , Mail = '" + mail + "'," + "pass = '" + pass + "'" + " where ClientID = " + clientID;
            excute_nonQuery(q);


        }

        public object get_branchid()
        {
            string q = "select BRANCH.BranchID from BRANCH";
            return Readtable(q);
        }

        public object getUser(int ID)
        {
            string q = "SELECT Client_FName, Client_LName ,Client_phone ,bdate,Mail,pass FROM CLIENT WHERE ClientID = " + ID;

            return Readtable(q);
        }
        public object GetPostedCar(int ID)
        {
            string q = "select VEHICLE.Brand,VEHICLE.Vehicle_No,VEHICLE.CarDescription,VEHICLE.CC_Rnage,VEHICLE.Color,VEHICLE.Year_Model,VEHICLE.Gearing,VEHICLE.Body_Style,USED_VEHICLE.Price,USED_VEHICLE.Kilometers,USED_VEHICLE.Posting_Date,USED_VEHICLE.Class FROM (USED_VEHICLE JOIN VEHICLE ON USED_VEHICLE.Vehicle_ID = VEHICLE.Vehicle_No ) JOIN Client_Posts ON Client_Posts.VehcileId = VEHICLE.Vehicle_No WHERE Client_Posts.ClientId = " + ID;
            return Readtable(q);
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
                return dt;
            }
        }


        public List<vehicle> GetVehicles(
            int id = 0,
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
            else if (Brand != "" && color != "")
            {
                Q = "select * from VEHICLE " +
                    "where Brand =  '" + Brand + "' and Color = '" + color + "'";
            }
            else if (Brand != "")
            {
                Q = "select * from VEHICLE " +
                    "where Brand =  '" + Brand + "'";
            }
            else if (color != "")
            {
                Q = "select * from VEHICLE " +
                    "where Color =  '" + color + "' ";
            }
            else
            {
                Q = "select * from VEHICLE ";
            }

            DataTable table = (DataTable)Readtable(Q);
            List<vehicle> returned = new List<vehicle>();
            foreach (DataRow row in table.Rows)
            {

                vehicle newVehicle = new vehicle(
                    row["iimage"] != null ? (byte[])row["iimage"] : new byte[] { 0x00 },
                   id: row["Vehicle_No"] != null ? (int)(row["Vehicle_No"]) : 0,
                   car_status: row["Car_Status"] != null ? row["Car_Status"].ToString() : "",
                   showroom: row["SHOWROOM"] != null ? row["SHOWROOM"].ToString() : "",
                   Brand: row["Brand"] != null ? row["Brand"].ToString() : "",
                   CC_Range: row["CC_Rnage"] != null ? (int)row["CC_Rnage"] : 0,
                   color: row["Color"] != null ? row["Color"].ToString() : "",
                   year_model: row["Year_Model"] != null ? (int)row["Year_Model"] : 0,
                   Gearing: row["Gearing"] != null ? row["Gearing"].ToString() : "",
                   Body_Style: row["Body_Style"] != null ? row["Body_Style"].ToString() : ""
                   );
                returned.Add(newVehicle);
            }
            return returned;
        }
        /*        private object readTable(string Q);
        */

        //public void insert_vechile(string Brand, int CC, string Color, int year_Model, string Gearing, string B_style, int price, int Km, int car_class)
        //{
        //    string q = "INSERT INTO VEHICLE(Car_Status,Brand,CC_Rnage,Color,Year_Model,Gearing,Body_Style)" +
        //        "VALUES('Used','" + Brand + "'," + CC + ",'" + Color + "'," + year_Model + ",'" + Gearing + "','" + B_style + "')";
        //    DateTime currentDate = DateTime.Now;
        //    string formattedDate = currentDate.ToString("yyyy-MM-dd");
        //    string q2 = "INSERT INTO USED_VEHICLE VALUES(" + 1 + "," + Km + "," + price + ",'" + formattedDate + "'," + car_class + ")";
        //    excute_nonQuery(q);
        //    excute_nonQuery(q2);
        //}

        public DataTable GetAvailableFilters(string filter)
        {
            string Q = "";
            if (filter == "Brands")
            {
                Q = "SELECT DISTINCT Brand FROM VEHICLE";
            }
            else if (filter == "Colors")
            {
                Q = "SELECT DISTINCT Color FROM VEHICLE";
            }
            else if (filter == "gearing")
            {
                Q = "SELECT DISTINCT Gearing FROM VEHICLE";
            }
            else if (filter == "MinMax")
            {
                Q = "SELECT MIN(p.Price) AS MinPrice, MAX(p.Price) AS MaxPrice\r\nFROM (\r\n" +
                    "    SELECT NEW_VEHICLE.Price, Vehicle_ID  FROM NEW_VEHICLE\r\n    UNION ALL\r\n" +
                    "    SELECT USED_VEHICLE.Price, Vehicle_ID FROM USED_VEHICLE\r\n) AS p\r\nJOIN VEHICLE" +
                    " v ON p.Vehicle_ID = v.Vehicle_No;";
            }
            else if (filter == "status")
            {
                Q = "SELECT DISTINCT Car_Status FROM VEHICLE";
            }
            else if (filter == "Year Model")
            {
                Q = "SELECT DISTINCT Year_Model FROM VEHICLE";
            }
            else if (filter == "warranty years")
            {
                Q = "SELECT DISTINCT Warranty_years FROM VEHICLE";
            }
            else if (filter == "warranty kilometers")
            {
                Q = "SELECT DISTINCT Warranty_Kilometers FROM VEHICLE";
            }
            else if (filter == "seats")
            {
                Q = "SELECT DISTINCT Seats FROM VEHICLE";
            }
            return (DataTable)Readtable(Q);
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


        public void insertServiceCenter(int id, string Name, string Address, string services, decimal latitude, decimal longitude, int stars)
        { //to return any data type
            //Insert into Services_Center values(1,'Ahmed', 'Zamalek Street', 'Nissan Tida issues', 30.04754894570406, 30.04754894570406, 4)

            string query = "insert into Services_Center values (" + id + ", '" + Name + "', '" + Address + "', '" + services + "' ," + latitude + "," + longitude + "," + stars + ")";
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

        public object ReadTuple(int ID, string tablename, string pKey)
        { //to return any data type
            string query = "select * " + " from " + tablename + " where " + pKey + " = " + ID + ";";
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


        public void updateServiceCenter(int id, string Name, string Address, string services, decimal latitude, decimal longitude, int stars)
        {

            string query = "update Services_Center set Name = '" + Name + "', Address = '" + Address + "', Services = '" + services + "'," + "latitude = " + latitude + "," + "longitude = " + longitude + "where ID = " + id + ";";
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

        public void insertCartWish(string table, int CId, int PId)
        { //to return any data type
            string query = "insert into " + table + " values (" + CId + "," + PId + ")";
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
        
        public object getCarNew(int CId, string tableName)
        { //to return any data type
            string query = "select Brand, name, Color, iimage,Year_Model, Price, NEW_VEHICLE.Vehicle_ID from (" +tableName + " join VEHICLE on VEHICLE.Vehicle_No = "+tableName+".vehichle_ID) join NEW_VEHICLE on VEHICLE.Vehicle_No = NEW_VEHICLE.Vehicle_ID where Customer_ID = " + CId;
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                dt.Load(sqlCommand.ExecuteReader());
                con.Close();
                return dt;
            }
            catch (SqlException) { con.Close(); return 0; }
        }


        public object getCarUsed(int CId, string tableName)
        { //to return any data type
            string query = "select Brand, name, Color, iimage, Year_Model, Price, USED_VEHICLE.Vehicle_ID from (" + tableName + " join VEHICLE on VEHICLE.Vehicle_No = " + tableName + ".vehichle_ID) join USED_VEHICLE on VEHICLE.Vehicle_No = USED_VEHICLE.Vehicle_ID where Customer_ID = " + CId;

            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                dt.Load(sqlCommand.ExecuteReader());
                con.Close();
                return dt;
            }
            catch (SqlException) { con.Close(); return 0; }
        }

        public void deleteCartVehicle(string tablename, int pId, int CId, string column)
        {
            string query = "delete from " + tablename + " where Customer_ID = " + CId + " and " + column + "=" + pId;
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException) { con.Close(); }
        }

    }
}

