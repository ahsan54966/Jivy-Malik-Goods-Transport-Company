using Jivy_Malik_Goods_Transport_Company.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Jivy_Malik_Goods_Transport_Company.ViewModel.Admin.RouteDetail
{
    public class RouteDetailCrudViewModel
    {
        string connection = ConfigurationManager.ConnectionStrings["MalikDb"].ConnectionString;
        public List<VehicleRoutes> ShowAllRoutes()
        {
            var Routs = new List<VehicleRoutes>();

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Routes_GetAllRoutes", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var RouteDetail = new VehicleRoutes();

                    RouteDetail.VehicleNumber = dr["VehicleNumber"].ToString();

                    RouteDetail.VehicleDriver = dr["VehicleDriver"].ToString();

                    RouteDetail.VehicleSize = dr["VehicleSize"].ToString();
                    RouteDetail.VehiclePhoneNumber = dr["VehiclePhoneNumber"].ToString();

                    RouteDetail.RouteId = int.Parse(dr["RouteId"].ToString());

                    RouteDetail.RouteDate = DateTime.Parse(dr["RouteDate"].ToString());

                    RouteDetail.RouteDestination = dr["RouteDestination"].ToString();
                    RouteDetail.VehicleReferenceCompany = dr["VehicleReferenceCompany"].ToString();
                    RouteDetail.VehicleDriverCNIC = dr["VehicleDriverCNIC"].ToString();

                    Routs.Add(RouteDetail);
                }
            }
            con.Close();

            return Routs;
        }

        public void AddNewRoutes(VehicleRoutes Rout)
        {

            var Action = "";

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd0 = new SqlCommand("sp_Vehicle_VehicleExist", con);
            cmd0.CommandType = CommandType.StoredProcedure;
            cmd0.Parameters.AddWithValue("@VehicleNumber", Rout.VehicleNumber);
            SqlDataReader sdr = cmd0.ExecuteReader();
            if (sdr.HasRows)
            {

                if (sdr.Read())
                {
                    if (sdr["VehicleDriver"] != Rout.VehicleDriver || sdr["VehiclePhoneNumber"] != Rout.VehiclePhoneNumber || sdr["VehicleSize"] != Rout.VehicleSize)
                    {
                        Action = "Update";
                    }
                }

            }
            else
            {
                Action = "Insert";
            }

            SqlCommand cmd1 = new SqlCommand("sp_Routes_NewRoutes", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@VehicleNumber", Rout.VehicleNumber);
            cmd1.Parameters.AddWithValue("@DriverName", Rout.VehicleDriver);
            cmd1.Parameters.AddWithValue("@DriverPhoneNumber", Rout.VehiclePhoneNumber);
            cmd1.Parameters.AddWithValue("@VehicleSize", Rout.VehicleSize);
            cmd1.Parameters.AddWithValue("@RouteDate", Rout.RouteDate);
            cmd1.Parameters.AddWithValue("@RouteDestination", Rout.RouteDestination);
            cmd1.Parameters.AddWithValue("@Action", Action);
            cmd1.ExecuteNonQuery();













        }

        public void AddRoutesUpdate(VehicleRoutes Rout)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd1 = new SqlCommand("sp_Routes_UpdateRoutes", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@VehicleNumber", Rout.VehicleNumber.Trim());
            cmd1.Parameters.AddWithValue("@DriverName", Rout.VehicleDriver.Trim());
            cmd1.Parameters.AddWithValue("@DriverPhoneNumber", Rout.VehiclePhoneNumber);
            cmd1.Parameters.AddWithValue("@VehicleSize", Rout.VehicleSize);
            cmd1.Parameters.AddWithValue("@RouteDate", Rout.RouteDate);
            cmd1.Parameters.AddWithValue("@RouteDestination", Rout.RouteDestination);
            cmd1.Parameters.AddWithValue("@RouteId", Rout.RouteId);
            cmd1.ExecuteNonQuery();
        }
    }
}