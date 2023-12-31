﻿using System.Data.SqlClient;
using PointOfSale.Models;
using System.Data;

namespace PointOfSale.DataLayer
{
    public class FloorDataLayer
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn;
        public FloorDataLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("sql-conn"));
        }
        public DataTable Getfloorbyuser(int userid)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand("getfloorbyuser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", userid);
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {

                        adapter.Fill(dt);
                    }
                    conn.Close();
                };
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error of type {ex.GetType()} encountered while getting user's floor data due to {ex.Message}");
            }
        }
        
        
        
        
        public int AssignFloor(int userid,int floorid, int assignedby)
        {
            try
            {
                int res = 2;
                using (SqlCommand cmd = new SqlCommand("Assignfloor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@floorid", floorid);
                    cmd.Parameters.AddWithValue("@assignedby", assignedby);
                    cmd.Parameters.Add("@return", SqlDbType.Int, 32);
                    cmd.Parameters["@return"].Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    res = (int)cmd.Parameters["@return"].Value;
                    conn.Close();
                };
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception of type {ex.GetType()} encountered while assigning floor to user due to {ex.Message}");
            }
        }
    }
}
