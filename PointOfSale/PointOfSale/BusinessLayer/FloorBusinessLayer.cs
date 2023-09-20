using PointOfSale.DataLayer;
using PointOfSale.Models;
using System.Data;
namespace PointOfSale.BusinessLayer
{
    public class FloorBusinessLayer
    {
        private readonly FloorDataLayer dl;
        public FloorBusinessLayer(IConfiguration configuration)
        {
            dl = new FloorDataLayer(configuration);
        }

        public List<Floor> Getfloorbyuser(int userid)
        {
            List<Floor> floors = new List<Floor>(); 
            try
            {
                DataTable dt=dl.Getfloorbyuser(userid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Floor floor = new Floor();
                        floor.FloorId = Convert.ToInt32(dr["floorId"]);
                        floor.FloorTitle = dr["floorTitle"].ToString();
                        floors.Add(floor);
                    }
                }
                return floors;
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception occurred in getting floors assigned to user {ex.Message}");
            }
        }
        public int AssignFloor(int userid,int floorid,int assignedby)
        {
            try
            {
                int res=dl.AssignFloor(userid, floorid, assignedby);
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error of type {ex.GetType()} occured while assigning floor to user {ex.Message}");
            }
        }
    }
}
