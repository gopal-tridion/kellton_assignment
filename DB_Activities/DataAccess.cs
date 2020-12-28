using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Activities
{
    public class Dataaccess
    {
        string conn = Data.Conn;

        //Method for Login  
        public bool CheckLogin(UserInfo User)
        {
            bool loginSuccess = false;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                User.Firstname = "";
                User.Lastname = "";
                User.Phone = "";
                if (string.IsNullOrWhiteSpace(User.Email) || string.IsNullOrWhiteSpace(User.Pwd))
                {
                    return false;
                }
                SqlCommand cmd = new SqlCommand("UserDataInsertUpdateDelete", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@firstName", User.Firstname);
                cmd.Parameters.AddWithValue("@lastName", User.Lastname);
                cmd.Parameters.AddWithValue("@userName", User.Email);
                cmd.Parameters.AddWithValue("@password", User.Pwd);
                cmd.Parameters.AddWithValue("@phone", User.Phone);
                cmd.Parameters.AddWithValue("@StatementType", "LoginCheck");
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    loginSuccess = true;
                }
                connection.Close();
            }
            return loginSuccess;
        }
        // Method for SignUp  
        public async Task<int> Signup(UserInfo User)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand("UserDataInsertUpdateDelete", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@firstName", User.Firstname);
                    cmd.Parameters.AddWithValue("@lastName", User.Lastname);
                    cmd.Parameters.AddWithValue("@userName", User.Email);
                    cmd.Parameters.AddWithValue("@password", User.Pwd);
                    cmd.Parameters.AddWithValue("@phone", User.Phone);
                    cmd.Parameters.AddWithValue("@StatementType", "Insert");
                    int read = cmd.ExecuteNonQuery();
                    connection.Close();
                    return 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> ItemInsert(Item item)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("ItemDataInsertUpdateDelete", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@itemName", item.itemName);
                cmd.Parameters.AddWithValue("@itemRate", item.itemRate);
                cmd.Parameters.AddWithValue("@itemQuantity", item.itemQty);
                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                int read = cmd.ExecuteNonQuery();
                connection.Close();
                return 1;

            }
        }

        public bool DeleteUser(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand("UserDataInsertUpdateDelete", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@firstName", "");
                    cmd.Parameters.AddWithValue("@lastName", "");
                    cmd.Parameters.AddWithValue("@userName", email);
                    cmd.Parameters.AddWithValue("@password", "");
                    cmd.Parameters.AddWithValue("@phone", "");
                    cmd.Parameters.AddWithValue("@StatementType", "Delete");
                    int read = cmd.ExecuteNonQuery();
                    connection.Close();
                    return true;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateUser(Profile User)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand("UserDataInsertUpdateDelete", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@firstName", User.Firstname);
                    cmd.Parameters.AddWithValue("@lastName", User.Lastname);
                    cmd.Parameters.AddWithValue("@userName", User.Email);
                    cmd.Parameters.AddWithValue("@password", "");
                    cmd.Parameters.AddWithValue("@phone", User.Phone);
                    cmd.Parameters.AddWithValue("@StatementType", "Update");
                    int read = cmd.ExecuteNonQuery();
                    connection.Close();
                    return true;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Profile> GetAllUsers()
        {
            List<Profile> active = new List<Profile>();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("select * from UserRegistration", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    active.Add(new Profile
                    {
                        Firstname = reader["FirstName"].ToString(),
                        Lastname = reader["LastName"].ToString(),
                        Email = reader["UserName"].ToString(),
                        Phone = reader["Phone"].ToString()
                    });
                }
                connection.Close();
                return active;
            }
        }

        public Profile GetSelectedUser(string email)
        {
            Profile active = new Profile();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                string commandText = "select * from UserRegistration where userName = '"+ email +"' ";
                SqlCommand cmd = new SqlCommand(commandText, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    active = new Profile
                    {
                        Firstname = reader["FirstName"].ToString(),
                        Lastname = reader["LastName"].ToString(),
                        Email = reader["UserName"].ToString(),
                        Phone = reader["Phone"].ToString()
                    };
                }
                connection.Close();
                return active;
            }
        }

        public bool DeleteItem(int itemId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand("ItemDataInsertUpdateDelete", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@itemName", "");
                    cmd.Parameters.AddWithValue("@itemRate", "");
                    cmd.Parameters.AddWithValue("@itemQty", "");
                    cmd.Parameters.AddWithValue("@itemId", itemId);
                    cmd.Parameters.AddWithValue("@StatementType", "Delete");
                    int read = cmd.ExecuteNonQuery();
                    connection.Close();
                    return true;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateItem(Item item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand("ItemDataInsertUpdateDelete", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@itemId", item.itemId);
                    cmd.Parameters.AddWithValue("@itemName", item.itemName);
                    cmd.Parameters.AddWithValue("@itemRate", item.itemRate);
                    cmd.Parameters.AddWithValue("@itemQty", item.itemQty);
                    cmd.Parameters.AddWithValue("@StatementType", "Update");
                    int read = cmd.ExecuteNonQuery();
                    connection.Close();
                    return true;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Item> GetAllItems()
        {
            List<Item> active = new List<Item>();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("select * from ItemRegistration", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    active.Add(new Item
                    {
                        itemId = Convert.ToInt32(reader["itemId"]),
                        itemName = reader["itemName"].ToString(),
                        itemRate = Convert.ToInt32(reader["itemRate"]),
                        itemQty = Convert.ToInt32(reader["itemQty"])
                    });
                }
                connection.Close();
                return active;
            }
        }

        public Item GetSelectedItem(int itemId)
        {
            Item active = new Item();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                string commandText = "select * from ItemRegistration where itemId = '" + itemId + "' ";
                SqlCommand cmd = new SqlCommand(commandText, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    active = new Item
                    {
                        itemId = Convert.ToInt32(reader["itemId"]),
                        itemName = reader["itemName"].ToString(),
                        itemRate = Convert.ToInt32(reader["itemRate"]),
                        itemQty = Convert.ToInt32(reader["itemQty"])
                    };
                }
                connection.Close();
                return active;
            }
        }
    }
}
