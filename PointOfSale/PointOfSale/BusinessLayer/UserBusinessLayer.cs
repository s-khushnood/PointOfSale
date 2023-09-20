using PointOfSale.DataLayer;
using PointOfSale.Models;
using System.Data;
namespace PointOfSale.BusinessLayer
{
    public class UserBusinessLayer
    {
        private readonly UserDataLayer dl;
        public UserBusinessLayer(IConfiguration configuration)
        {
            dl = new UserDataLayer(configuration);
        }
        public User LoginUser(UserLog userLog)
        {
            User user=new User();
            try
            {
                DataTable dt = new DataTable();
                dt=dl.UserLogin(userLog);
                if(dt!=null && dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        user.UserId = Convert.ToInt32(dr["userId"]);
                        user.firstName=dr["firstName"].ToString();
                        user.lastName=dr["lastName"].ToString() ;
                        user.CNIC = dr["CNIC"].ToString();
                        user.PhoneNo = dr["phoneNo"].ToString();
                        user.UserMail = dr["userMail"].ToString();
                        user.IsActive = Convert.ToBoolean(dr["isActive"]);
                        user.IsAdmin = Convert.ToBoolean(dr["isAdmin"]);
                        user.UserCreated = Convert.ToDateTime(dr["userCreateTime"]);
                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error encountered of type {ex.GetType()} due to {ex.Message} in user login");
            }
        }
        public string UserSignup(User user)
        {
            try
            {
                string response =dl.UserSignup(user);
                return response;
            }
            catch(Exception ex)
            {
                throw new Exception($"Error encountered of type {ex.GetType()} due to {ex.Message} in user login");
            }
        }
        public string UserUpdate(Userupdate userupdate)
        {
            try
            {
                string response = dl.UserUpdate(userupdate);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error encountered of type {ex.GetType()} due to {ex.Message} in user update");
            }
        }
        public int UserAdminToggle(int userid,int updateid)
        {
            try
            {
                int response=2;
                response = dl.UserAdminToggle(userid, updateid);
                return response;
            }
            catch(Exception ex)
            {
                throw new Exception($"Error encountered of type {ex.GetType()} due to {ex.Message} in user admin toggle");
            }
        }
    }
}
