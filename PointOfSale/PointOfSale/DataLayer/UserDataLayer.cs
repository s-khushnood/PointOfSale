using System.Data.SqlClient;
using PointOfSale.Models;
using System.Data;

namespace PointOfSale.DataLayer
{
    public class UserDataLayer
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public UserDataLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("sql-conn"));
        }
        public DataTable UserLogin(UserLog userLog)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand("UserLogin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email",userLog.UserMail);
                    cmd.Parameters.AddWithValue("@pword",userLog.Password);
                    conn.Open();
                    using(SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                       
                        adapter.Fill(dt);
                    }
                    conn.Close();
                };
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception of type {ex.GetType()} encountered while login user in due to {ex.Message}");
            }
        }

        public string UserSignup(User user)
        {
            try
            {
                string response = "";
                using (SqlCommand cmd = new SqlCommand("CreateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@firstname", user.firstName);
                    cmd.Parameters.AddWithValue("@lastname", user.lastName);
                    cmd.Parameters.AddWithValue("@cnic", user.CNIC);
                    cmd.Parameters.AddWithValue("@phone", user.PhoneNo);
                    cmd.Parameters.AddWithValue("@email", user.UserMail);
                    cmd.Parameters.AddWithValue("@pword", user.Password);
                    cmd.Parameters.AddWithValue("@isadmin", Convert.ToBoolean(user.IsAdmin));
                    cmd.Parameters.AddWithValue("createdby", Convert.ToInt32(user.Createdby));
                    cmd.Parameters.Add("@return", SqlDbType.Int, 32);
                    cmd.Parameters["@return"].Direction = ParameterDirection.Output;
                    conn.Open();
                    response=cmd.ExecuteNonQuery().ToString();
                    int res= (int)cmd.Parameters["@return"].Value;
                    if (res == -1)
                    {
                        return response="Not An Admin";
                    }
                    conn.Close();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception of type {ex.GetType()} encountered while user signup due to {ex.Message}");
            }
        }

        public string UserUpdate(Userupdate userupdate)
        {
            try
            {
                string response = "";
                using (SqlCommand cmd = new SqlCommand("updateuser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", userupdate.UserId);
                    cmd.Parameters.AddWithValue("@firstname", userupdate.firstName);
                    cmd.Parameters.AddWithValue("@lastname", userupdate.lastName);
                    cmd.Parameters.AddWithValue("@phoneno", userupdate.PhoneNo);
                    cmd.Parameters.AddWithValue("@pword", userupdate.Password);
                    cmd.Parameters.AddWithValue("@updateby", Convert.ToInt32(userupdate.Updatedby));
                    conn.Open();
                    response = cmd.ExecuteNonQuery().ToString();
                    conn.Close();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception of type {ex.GetType()} encountered while user update due to {ex.Message}");
            }
        }
        public int UserAdminToggle(int userid, int updateid)
        {
            try
            {
                int res = 2;
                using (SqlCommand cmd = new SqlCommand("useradmintoggle", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid",userid);
                    cmd.Parameters.AddWithValue("@updateby", updateid);
                    cmd.Parameters.Add("@return", SqlDbType.Int, 32);
                    cmd.Parameters["@return"].Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    res = (int)cmd.Parameters["@return"].Value;
                    conn.Close();
                };
                return res;
            }
            catch(Exception ex)
            {
                throw new Exception($"An exception of type {ex.GetType()} encountered while changing user's admin status due to {ex.Message}");
            }
        }
    }
}
