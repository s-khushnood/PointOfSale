using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLayer;
using PointOfSale.Models;
using System.Security.Cryptography;
using System.Text;

namespace PointOfSale.Controllers
{
    [ApiController]
    public class FloorController : ControllerBase
    {
        private readonly FloorBusinessLayer bl;
        public FloorController(IConfiguration configuration)
        {
            bl = new FloorBusinessLayer(configuration);
        }
        [HttpGet]
        [Route(@"getfloorbyuser")]
        public async Task<IActionResult> Getfloorbyuser(int userid)
        {
            try
            {
                List<Floor> floors =new List<Floor>();
                floors=bl.Getfloorbyuser(userid);
                if (floors != null)
                {
                    return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Successfully fectched floors assigned to user", floors });
                }
                return BadRequest(new {StatusCode=StatusCodes.Status400BadRequest,Message="No floor assigned to this user"});
                    
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = StatusCodes.Status500InternalServerError, Message = $"Exception occurred: {ex.Message}" });
            }
        }


        [HttpPost]
        [Route(@"assignfloor")]
        public async Task<IActionResult> UserFloorAssign(int userid, int floorid,int assignedby)
        {
            try
            {
                int response = new();
                if (userid > 0 && assignedby > 0 && floorid>0)
                {
                    response = bl.AssignFloor(userid, floorid, assignedby);
                    if (response == 1)
                    {
                        return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Floor has been assigned to user" });
                    }
                    else if (response == 0)
                    {
                        return BadRequest(new { StatusCode = StatusCodes.Status403Forbidden, Message = "Floor already assigned to this user" });
                    }
                    else if (response == -1)
                    {
                        return BadRequest(new { StatusCode = StatusCodes.Status403Forbidden, Message = "Only admins can assign floors to users" });
                    }
                }
                return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Message = "Invalid UserId, FloorId or AdminId" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = StatusCodes.Status500InternalServerError, Message = $"Exception occurred: {ex.Message}" });
            }
        }
    }
}
