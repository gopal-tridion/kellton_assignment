using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseActivities
{
    public class Dataaccess
    {
        string conn = Data.Conn;

        //Method for Login  
        public bool CheckLogin(UserInfo user)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {

                if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
                {
                    return false;
                }
                SqlCommand cmd = new SqlCommand("loginForm", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Username1", user.UserName);
                cmd.Parameters.AddWithValue("@Password1", user.Password);
                object read = cmd.ExecuteScalar();
                int Var = Convert.ToInt32(read);
                connection.Close();
                return Var == 1;
            }

        }
        // Method for SignUp  
        public bool Signup(UserInfo User)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Signinform", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Username1", User.UserName);
                cmd.Parameters.AddWithValue("@Password1", User.Password);
                cmd.Parameters.AddWithValue("@Mail", User.Mail);
                int read = cmd.ExecuteNonQuery();
                connection.Close();
                return read == 1;

            }
        }
    }
} 
