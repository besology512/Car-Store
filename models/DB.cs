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
            string conString = "Data Source=DESKTOP-KDC2LT0;Initial Catalog=THECARSTORE;Integrated Security=True";
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
        public void insertShowRoom(string Name, string City, string street, string phoneNumber)
        { //to return any data type
            string query = "insert into SHOWROOM values ('" + Name + "', '" + City + "', '" + street + "', '" + phoneNumber +"')";
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
            string query = "select * " + " from " + tablename+ " where " + pKey + " = " + "'"+Name+"';";
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
        public void updateTupleRoom(string PreviousName, string Name, string City, string street, string phoneNumber) {
            string query = "update SHOWROOM set Show_Room_Name = '" + Name + "', City = '"+City+ "', STreet = '"+street+ "', phoneNumber = '"+phoneNumber+ "' where Show_Room_Name = '"+PreviousName+"';";
            try {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.ExecuteNonQuery();
                con.Close();
            }catch(SqlException) { con.Close();}
        }
        public void deletetuple(string tableName, string pKey, string pColumn) {
            string query = "delete from " + tableName + " where " + pColumn + " = '" + pKey + "';";
            try {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.ExecuteNonQuery();
                con.Close();
            }catch(SqlException ex) { con.Close();}
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

    }
}
