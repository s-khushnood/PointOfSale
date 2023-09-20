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
