using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Security.Cryptography;
using Org.BouncyCastle.Asn1.X509;
using System.Data.Common;
using Org.BouncyCastle.Asn1.Cms;
using System.IO;

namespace Car_Store.models
{

    public class DB
    {
        SqlConnection con;

        public DB()
        {

            string conString = "Data Source=SQL6031.site4now.net;Initial Catalog=db_a99893_trmbcar;User Id=db_a99893_trmbcar_admin;Password=TR$$MB$$8865";



            con = new SqlConnection(conString);
        }

        public string date1 { get; set; }
        public List<DataPoint> GetData(string query)
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DataPoint dataPoint = new DataPoint(dr["Id"], dr["XValue"], dr["YValue"]);
                    dataPoints.Add(dataPoint);
                }
            }
            con.Close();
            return dataPoints;
        }

        public void insertUser(string Fname, string Lname, string pass, string phoneNumber, string date, string email, string UserName)
        { //to return any data type
            /*            string date1 = date.ToString();
            */

            string query = "insert into CLIENT (Client_Username,Client_FName,Client_LName,Client_image,Client_phone,pass,bdate,Mail,UserType) values ('" + UserName + "','" + Fname + "', '" + Lname + "','images/facebook-default-no-profile-pic.jpg','" + phoneNumber + "', '" + pass + "', '" + date + "', '" + email + "', 0)";
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
        


        public void insert_vechile(string Brand, int CC, string Color, int year_Model, string Gearing, string B_style, int price, int Km, int car_class, string cardes, string name, string fuel, string city, string path)
        {
            string q = "INSERT INTO VEHICLE(Car_Status,Brand,CC_Rnage,Color,Year_Model,Gearing,Body_Style,CarDescription,name,Fuel,visibality,C_image1)" +
            "VALUES('Used','" + Brand + "'," + CC + ",'" + Color + "'," + year_Model + ",'" + Gearing + "','" + B_style + "','" + cardes
            + "','" + name + "','" + fuel + "'," + 0 + ", '" + path + "' )";

            DateTime currentDate = DateTime.Today;

            string formattedDate = currentDate.ToString("yyyy-MM-dd");

            excute_nonQuery(q);

            int vecId = getTopVehicleId();
            
                

            string q2 = "INSERT INTO USED_VEHICLE VALUES(" + vecId + "," + Km + "," + price + ",'" + formattedDate + "'," + car_class + ",'" + city + "'," + 1 + ")";

            excute_nonQuery(q2);
        }
        public void admin_add_car(string Brand, int CC, string Color, int year_Model, string Gearing, string B_style,int Price ,int car_class, string cardes, string name, string fuel, string path , int count)
        {
            string q = "INSERT INTO VEHICLE(Car_Status,Brand,CC_Rnage,Color,Year_Model,Gearing,Body_Style,CarDescription,name,Fuel,visibality,C_image1)" +
            "VALUES('new','" + Brand + "'," + CC + ",'" + Color + "'," + year_Model + ",'" + Gearing + "','" + B_style + "','" + cardes
            + "','" + name + "','" + fuel + "'," + 1 + ", '" + path + "' )";

            excute_nonQuery(q);
            int vecId = getTopVehicleId();

            string q2 = "insert into NEW_VEHICLE (Vehicle_ID,Price,class,count) VALUES (" + vecId + "," + Price + "," + car_class + "," + count + ")";
            excute_nonQuery(q2);

        }

        public void insert_to_pendingposts(int clientID, int vehcId)
        {
            string q = "insert into PENDING_POSTS(CLIENT_ID,VEHCILE_ID) Values (" + clientID + "," + vehcId + ")";
            excute_nonQuery(q);
        }
        public int getTopVehicleId()
        {
            string query = "SELECT IDENT_CURRENT('vehicle') AS CurrentIdentityValue";
            int t = (int)getsinglevalue(query);
            return t;
        }
        public void insert_CLIENT_POSTS(int clientid, int vehicleId)

        {
            string q = "INSERT INTO Client_Posts VALUES(" + clientid + " , " + vehicleId + ")";
            excute_nonQuery(q);
        }
        public int check_user_name(string uname,string mail)
        {
            string q = "select count(*) as user_countt from CLIENT where Client_Username = '" + uname + "' or CLIENT.Mail = '" + mail + "'";
            return Convert.ToInt32(getsinglevalue(q));
        }

        public void delet_CLIENT_POST(int vecId, int ClieTnID)
        {
            string q = "delete from Client_Posts where ClientId =  " + ClieTnID + " AND " + "VehcileId = " + vecId;
            string q2 = "delete from VEHICLE where VEHICLE.Vehicle_No = " + vecId;
            excute_nonQuery(q);
            excute_nonQuery(q2);
        }
        public void Delete_Car_by_admin(int vecId)
        {
            string q = "delete from VEHICLE where VEHICLE.Vehicle_No = " + vecId;
            excute_nonQuery(q);
        }
        public object get_car_statistics()
        {
            string q = "select count(*) as car_count , VEHICLE.Car_Status  from VEHICLE where VEHICLE.visibality != 0 group by VEHICLE.Car_Status ";
            return Readtable(q);
            
        }
        public int get_all_cars_count()
        {
            string q = "select count(*) as car_count from VEHICLE ";
            int t = (int)getsinglevalue(q);
            return t;
        }
        public void insert_emp(string Username,string Fname,string Mname,string Lname, string password,string SSN,string Bdate,string Gender, int salary,int super_id , int usertype, string jobType,int branch,int deparmentID)
        {
            string q = "Insert into EMPLOYEE values('"+Username+ "','"+Fname+"','"+Mname+"','"+Lname+"','"+password+"','"+SSN+"','"+Bdate+"','"+Gender+"',"+salary+","+super_id+","+usertype+",'"+jobType+"')";
            excute_nonQuery(q);
            string query = "SELECT IDENT_CURRENT('EMPLOYEE') AS CurrentIdentityValue";
            int counter = Convert.ToInt32(getsinglevalue(query));
            string q2 = "insert into works_in_branchdep values("+counter+","+branch + "," +deparmentID+")";
            excute_nonQuery(q2);
        }

        public void updateEmployee(int id, string Username, string Fname, string Mname, string Lname, string password, string SSN, string Bdate, string Gender, int salary, int super_id, int usertype, string jobType, int branch, int deparmentID)
        {
            string q = "UPDATE employee SET Emp_Username = '" + Username + "', pass = '" + password + "', Fname = '" + Fname + "' ,Mname = '" + Mname + "' , Lname = '" + Lname + "', SSN = '" + SSN + "', BirthDate= '" + Bdate + "', Gender = '" + Gender + "', SuperID =" + super_id + ", JOB_TYPE = '" + jobType + "', Salary = " + salary + ", UserType = " + usertype + " WHERE ID = " + id;
            excute_nonQuery(q);
            string query = "SELECT IDENT_CURRENT('EMPLOYEE') AS CurrentIdentityValue";
            int counter = Convert.ToInt32(getsinglevalue(query));
            string q2 = "Update works_in_branchdep Set Dep_ID =" + deparmentID + ", Branch_ID = " + branch+ " where Emp_ID = " + id;
            excute_nonQuery(q2);
        }

        public object get_department_num()
        {
            string q = "select * from DEPARTMENT";
            return Readtable(q);
        }
        public object get_all_branches_num()
        {
            string q = "select  BRANCH.BranchID  from BRANCH";
            return Readtable(q);
        }

        public void insert_product(string category, int branchId, int qunatity, string brand, int price, int status, string description, int pid)
        {
            string q = "INSERT INTO PRODUCT (CATEGORY,BranchNo,Quantity,brand,Price,Statuss,Product_title,ProductID) VALUES('" + category + "'," + branchId + "," + qunatity + ",'" + brand + "'," + price + "," + status + ",'" + description + "'," + pid + ")";
            excute_nonQuery(q);
        }

        public void approve_car(int carid, int clientID)
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
            string q = "select  VEHICLE.CarDescription, USED_VEHICLE.Posting_Date, USED_VEHICLE.Price, VEHICLE.CC_Rnage, PENDING_POSTS.VEHCILE_ID, CLIENT.Client_Username , PENDING_POSTS.CLIENT_ID, VEHICLE.C_image1 " +
                "from (VEHICLE JOIN USED_VEHICLE ON VEHICLE.Vehicle_No = USED_VEHICLE.Vehicle_ID) " +
                "JOIN PENDING_POSTS  ON VEHICLE.Vehicle_No = PENDING_POSTS.VEHCILE_ID JOIN CLIENT ON CLIENT.ClientID = PENDING_POSTS.CLIENT_ID";
            return Readtable(q);
        }

        public void edit_client_info(int clientID, string Fname, string Lname, string phone, string mail, string pass , string img)
        {
            string q = "update CLIENT set Client_FName = '" + Fname + "'" +
                " , Client_LName = '" + Lname + "'" + " , Client_phone = '" + phone + "'"
                + " , Mail = '" + mail + "'," + "pass = '" + pass + "' , " + "Client_image = '" + img + "'" + " where ClientID = "
                + clientID;
            excute_nonQuery(q);


        }
        public object get_all_cars()
        {
            string q = "SELECT VEHICLE.Vehicle_No, name,Car_Status," +
                "COALESCE(NEW_VEHICLE.count, USED_VEHICLE.count) as count," +
                "Brand, CC_Rnage, Color, Year_Model, Gearing, Body_Style, Fuel, Seats, CarDescription, C_image1, visibality" +
                ",COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) AS Price , Kilometers, Posting_Date, " +
                "COALESCE(NEW_VEHICLE.Class,USED_VEHICLE.Class)AS Class ," +
                "CITY " + 
                "FROM VEHICLE LEFT JOIN NEW_VEHICLE ON VEHICLE.Vehicle_No = NEW_VEHICLE.Vehicle_ID LEFT JOIN USED_VEHICLE  ON VEHICLE.Vehicle_No = " +
                "USED_VEHICLE.Vehicle_ID where visibality != 0 ";
            return Readtable(q);
            
        }

        public object get_branchid()
        {
            string q = "select BRANCH.BranchID from BRANCH";
            return Readtable(q);
        }

        public object getUser(int ID)
        {
            string q = "SELECT Client_FName, Client_LName ,Client_phone ,bdate,Mail,pass, Client_image FROM CLIENT WHERE ClientID = " + ID;

            return Readtable(q);
        }
        public object GetPostedCar(int ID)
        {
            string q = "select VEHICLE.Brand," +
                "VEHICLE.Vehicle_No," +
                "VEHICLE.CarDescription," +
                "VEHICLE.CC_Rnage," +
                "VEHICLE.Color," +
                "VEHICLE.Year_Model," +
                "VEHICLE.Gearing," +
                "VEHICLE.Body_Style," +
                "USED_VEHICLE.Price," +
                "USED_VEHICLE.Kilometers," +
                "USED_VEHICLE.Posting_Date," +
                "USED_VEHICLE.Class," +
                " VEHICLE.C_image1 FROM (USED_VEHICLE JOIN VEHICLE ON USED_VEHICLE.Vehicle_ID = VEHICLE.Vehicle_No ) JOIN Client_Posts ON Client_Posts.VehcileId = VEHICLE.Vehicle_No WHERE Client_Posts.ClientId = " + ID;
            return Readtable(q);
        }

        public object get_all_orders_admin()
        {
            string q = "SELECT ORDERS.Order_No," +
                "ORDERS.order_date," +
                "ORDERS.order_status," +
                "ORDERS.City," +
                "ORDERS.Street," +
                "ORDERS.Building," +
                "ORDERS.HouseNo," +
                "ORDERS.ClientID," +
                "orderItems.vehichle_ID," +
                "CLIENT.Client_FName + CLIENT.Client_LName AS C_NAME" +
                " ,VEHICLE.C_image1 " +
                "FROM ((ORDERS join orderItems on ORDERS.ClientID = orderItems.Customer_ID) JOIN " +
                "CLIENT ON orderItems.Customer_ID = CLIENT.ClientID) JOIN VEHICLE " +
                "ON VEHICLE.Vehicle_No = orderItems.vehichle_ID order by Order_No";
            return Readtable(q);
        }

        public int get_num_of_order()
        {
            string q = "select count(*) as count from ORDERS";
            return Convert.ToInt32(getsinglevalue(q));
        }

        public void update_order_status(string status,int orderID)
        {
            string q = "update ORDERS set order_status = '" + status + "' where Order_No = " + orderID;
            excute_nonQuery(q);
        }
        public void del_temp()
        {
            string q = "delete from VEHICLE";
            excute_nonQuery(q);
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
            List<string> Seats,
            string car_status = "",
            string showroom = "",
            string Brand = "",
            string color = "",
            string gearing = "",
            int maxPrice = 0,
            int minPrice = 0,
           string status = ""
            )
        {
            string Q = "SELECT VEHICLE.Vehicle_No,name,rating,Car_Status,COALESCE(NEW_VEHICLE.count, USED_VEHICLE.count) as count,SHOWROOM,Brand,CC_Rnage, Color, Year_Model, Gearing, Body_Style, Engine_Capacity, hourse_power, maximum_speed, Warranty_years, Warranty_Kilometers, acceleration, speeds, Fuel, Liter_per_100KM, width, height, Trunk_Size, Seats, Traction_Type, Cylinders, CarDescription, Tank_Capacity, C_image1, C_image2, C_image3, visibality,COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) AS Price, Kilometers, Posting_Date, COALESCE(NEW_VEHICLE.Class,USED_VEHICLE.Class)AS Class ,CITY FROM VEHICLE LEFT JOIN NEW_VEHICLE ON VEHICLE.Vehicle_No = NEW_VEHICLE.Vehicle_ID LEFT JOIN USED_VEHICLE  ON VEHICLE.Vehicle_No = USED_VEHICLE.Vehicle_ID ";

            if (Brand != "" && color != "" && gearing != "" && status != "")
            {
                Q +=
                "where Brand =  '" + Brand + "' and Color = '" + color + "'" + "and Car_Status = '" + status + "' " + "and Gearing = '" + gearing + "' and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (Brand != "" && color != "" && gearing != "")
            {
                Q +=
                "where Brand =  '" + Brand + "' and Color = '" + color + "'" + "and Gearing = '" + gearing + "' and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (Brand != "" && color != "" && status != "")
            {
                Q +=
                "where Brand =  '" + Brand + "' and Color = '" + color + "'" + "and Car_Status = '" + status + "' " + " and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (Brand != "" && color != "")
            {
                Q +=
                "where Brand =  '" + Brand + "' and Color = '" + color + "'" + " and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (Brand != "" && gearing != "" && status != "")
            {
                Q +=
                "where Brand =  '" + Brand + "'" + " and Car_Status = '" + status + "' " + " and Gearing = '" + gearing + "' and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (Brand != "" && gearing != "")
            {
                Q +=
                "where Brand =  '" + Brand + "'" + "and Gearing = '" + gearing + "' and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (Brand != "" && status != "")
            {
                Q +=
                "where Brand =  '" + Brand + "'" + " and Car_Status = '" + status + "' " + " and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (Brand != "")
            {
                Q +=
                "where Brand =  '" + Brand + "'" + " and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (color != "" && gearing != "" && car_status != "")
            {

                Q += "where Color =  '" + color + "'" + " and Gearing = '" + gearing + "'"
                    + " and Car_Status = '" + status + "' " + " and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (color != "" && gearing != "")
            {

                Q += "where Color =  '" + color + "'" + " and Gearing = '" + gearing + "'"
                    + " and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (color != "" && status != "")
            {
                Q += "where Color =  '" + color + "'" + " and Car_Status = '" + status + "' "
                    + " and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (color != "")
            {
                Q += "where Color =  '" + color + "'"
                    + " and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (gearing != "" && status != "")
            {
                Q += "where Gearing =  '" + gearing + "'" + " and Car_Status = '" + status + "' "
                       + " and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (gearing != "")
            {
                Q += "where Gearing =  '" + gearing + "'"
                       + " and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else if (status != "")
            {
                Q += "where Car_Status = '" + status + "' " + " and COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            else
            {
                Q += "WHERE COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price) BETWEEN " + minPrice + " AND " + maxPrice;
            }
            if (Seats.Count != 0)
            {
                string Q2 = "Seats IN (";

                for (int i = 0; i < Seats.Count - 1; i++)
                {
                    Q2 += Seats[i] + ",";
                }
                Q2 += Seats[Seats.Count - 1] + ")";
                Q += " and " + Q2;
            }

            Q += " AND visibality <> 0";
            Console.WriteLine(Q);

            DataTable table = (DataTable)Readtable(Q);
            List<vehicle> returned = new List<vehicle>();
            foreach (DataRow row in table.Rows)
            {

                vehicle newVehicle = new vehicle(
                   car_path: row["C_image1"] is not System.DBNull ? row["C_image1"].ToString() : "",
                   car_image2: row["C_image2"] is not System.DBNull ? row["C_image2"].ToString() : "",
                   car_image3: row["C_image3"] is not System.DBNull ? row["C_image3"].ToString() : "",
                   id: row["Vehicle_No"] is not System.DBNull ? (int)(row["Vehicle_No"]) : 0,
                   car_status: row["Car_Status"] is not System.DBNull ? row["Car_Status"].ToString() : "",
                   showroom: row["SHOWROOM"] is not System.DBNull ? row["SHOWROOM"].ToString() : "",
                   Brand: row["Brand"] is not System.DBNull ? row["Brand"].ToString() : "",
                   CC_Range: row["CC_Rnage"] != null ? (int)row["CC_Rnage"] : 0,
                   color: row["Color"] is not System.DBNull ? row["Color"].ToString() : "",
                   year_model: row["Year_Model"] != null ? (int)row["Year_Model"] : 0,
                   Gearing: row["Gearing"] is not System.DBNull ? row["Gearing"].ToString() : "",
                   Body_Style: row["Body_Style"] is not System.DBNull ? row["Body_Style"].ToString() : "",
                   Rating: row["rating"] is not System.DBNull ? (int)row["rating"] : new Random().Next(0, 6),
                   visibility: row["visibality"] is not System.DBNull ? (int)row["visibality"] : 0,
                   Price: row["Price"] is not System.DBNull ? (int)row["Price"] : 0,
                   Count: row["count"] is not System.DBNull ? (int)row["count"] : 0
                   
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
            else if (filter == "Seats")
            {
                Q = "SELECT DISTINCT Seats FROM VEHICLE";
            }
            else if (filter == "Colors")
            {
                Q = "SELECT DISTINCT Color FROM VEHICLE";
            }
            else if (filter == "gearing")
            {
                Q = "SELECT DISTINCT Gearing FROM VEHICLE";
            }
            else if (filter == "Price")
            {
                Q = "SELECT MAX( COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price)) as Max, MIN( COALESCE(NEW_VEHICLE.Price, USED_VEHICLE.Price))  as Min FROM  VEHICLE LEFT JOIN NEW_VEHICLE ON VEHICLE.Vehicle_No = NEW_VEHICLE.Vehicle_ID LEFT JOIN USED_VEHICLE ON VEHICLE.Vehicle_No = USED_VEHICLE.Vehicle_ID";
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


        public void insertServiceCenter(string Name, string Address, string services, int PhoneNumber, decimal latitude, decimal longitude, int stars)
        { //to return any data type
            //Insert into Services_Center values(1,'Ahmed', 'Zamalek Street', 'Nissan Tida issues', 30.04754894570406, 30.04754894570406, 4)

            string query = "insert into Services_Center values (" + "'" + Name + "', '" + Address + "', '" + services + "'," + PhoneNumber + "," + latitude + "," + longitude + "," + stars + ")";
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
        public object Select(string Q)
        {
            return FunctionReaderExecute(Q);
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


        public void updateServiceCenter(int id, string Name, string Address, string services, int PhoneNumber ,decimal latitude, decimal longitude, int stars)
        {

            string query = "update Services_Center set Name = '" + Name + "', Address = '" + Address + "', Services = '" + services + "',  PhoneNumber = " + PhoneNumber + ", latitude = " + latitude + "," + "longitude = " + longitude + "where ID = " + id + ";";
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
            string query = "select Brand, name, Color, C_image1,Year_Model, Price, NEW_VEHICLE.Vehicle_ID from (" + tableName + " join VEHICLE on VEHICLE.Vehicle_No = " + tableName + ".vehichle_ID) join NEW_VEHICLE on VEHICLE.Vehicle_No = NEW_VEHICLE.Vehicle_ID where Customer_ID = " + CId;
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
            string query = "select Brand, name, Color, C_image1, Year_Model, Price, USED_VEHICLE.Vehicle_ID from (" + tableName + " join VEHICLE on VEHICLE.Vehicle_No = " + tableName + ".vehichle_ID) join USED_VEHICLE on VEHICLE.Vehicle_No = USED_VEHICLE.Vehicle_ID where Customer_ID = " + CId+ " and visibality = 1";

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

        public object getNewCar(int CId)
        { //to return any data type
            string query = "select v.name, v.brand, v.CC_Rnage, v.Color, v.Year_Model, v.Gearing, v.Body_Style, V.Fuel, v.CarDescription, v.C_image1, n.Price, n.count from Vehicle v join NEW_Vehicle n on v.Vehicle_No = n.Vehicle_ID where n.Vehicle_ID = " + CId + ";";

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

        public object getUsedCar(int CId)
        { //to return any data type
            string query = "select v.name, v.brand, v.CC_Rnage, v.Color, v.Year_Model, v.Gearing, v.Body_Style, V.Fuel, v.CarDescription, v.C_image1, u.Kilometers, u.Price, u.Class, u.City from Vehicle v join Used_vehicle u on v.Vehicle_no = u.Vehicle_ID where u.Vehicle_ID = " + CId + ";";

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
        public void order(string city, string street, string building, string house, int Id) {
            string currentDate = DateTime.Today.ToString();
            string time = DateTime.Now.TimeOfDay.ToString();
            string query = "insert into ORDERS(order_date, order_time, order_status, City, Street, Building, HouseNo, ClientId) values ('"+currentDate+"', '"+time+"', 'Processing', '"+city+"', '"+street+"', '"+building+"', '"+house+"', "+Id+")";
            string query2 = "delete from Cart_vehicle where Cart_vehicle.Customer_ID = "+Id;
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                SqlCommand sqlCommand2 = new SqlCommand(query2, con);
                sqlCommand.ExecuteNonQuery();
                sqlCommand2.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException) { con.Close(); }
        }
        public void purchaseCar(int Vid, int Cid) {
            string query = "insert into OrderItems values ("+Cid+", "+Vid+")";
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException) { con.Close(); }
        }

        public void minusNewCar(int Vid) {
            string query = "update NEW_VEHICLE set count = count-1 where Vehicle_ID = " + Vid;
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException) { con.Close(); }
        }

        public void notVisible(int Vid) {
            string query = "update VEHICLE set visibality = 0 where Vehicle_No = " + Vid;
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException) { con.Close(); }
        }

        public object getOrders(int CID) {
            string query = "select distinct VEHICLE.Brand, VEHICLE.name, VEHICLE.Color, orders.order_status, Vehicle.C_image1 from (orderItems join VEHICLE on orderItems.vehichle_ID = vehicle.Vehicle_No) join ORDERS on orderItems.Customer_ID = ORDERS.ClientID where Customer_ID = " + CID;
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
    }
}

