using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLayer;
using PointOfSale.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserBusinessLayer bl;
        public LoginController(IConfiguration configuration)
        {
            bl = new UserBusinessLayer(configuration);
        }

        [HttpPost]
        [Route("loginuser")]
        public async Task<IActionResult> LoginUser([FromBody] UserLog userLog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userLog.Password = GetHash256(userLog.Password);
                    User user = bl.LoginUser(userLog);
                    if (user.UserMail == null)
                    {
                        return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Message = "User Not Found" });
                    }
                    return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Login Successful" ,user});
                }
                return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Message = "Invalid Inputs" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = StatusCodes.Status500InternalServerError, Message = $"Exception occurred: {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("usersignup")]
        public async Task<IActionResult> UserSignup([FromBody] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Password = GetHash256(user.Password);
                    string response = bl.UserSignup(user);
                    if (response == null)
                    {
                        return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Message = "User Not added" });
                    }
                    return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "User Added successfully" });
                }
                return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Message = "Invalid Inputs" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = StatusCodes.Status500InternalServerError, Message = $"Exception occurred: {ex.Message}" });
            }
        }

        [HttpPut]
        [Route("userupdate")]
        public async Task<IActionResult> UserUpdate([FromBody] Userupdate userupdate)
        {
            try
            {
                if (userupdate.Password != null)
                {
                    userupdate.Password = GetHash256(userupdate.Password);
                }
                if (ModelState.IsValid)
                {
                    string response = bl.UserUpdate(userupdate);
                    if (response == null)
                    {
                        return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Message = "Data Not Updated" });
                    }
                    return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "User data updated successfully" });
                }
                return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Message = "Invalid Inputs" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = StatusCodes.Status500InternalServerError, Message = $"Exception occurred: {ex.Message}" });
            }
        }

        static string GetHash256(string s)
        {
            string Hash = string.Empty;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashvalue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
                foreach (byte b in hashvalue)
                {
                    Hash += $"{b:X2}";
                }
            }
            return Hash;
        }
    }
}
