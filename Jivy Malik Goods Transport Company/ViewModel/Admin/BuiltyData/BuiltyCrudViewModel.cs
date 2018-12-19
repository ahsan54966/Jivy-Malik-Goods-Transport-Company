using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Jivy_Malik_Goods_Transport_Company.Models;
using Jivy_Malik_Goods_Transport_Company.Reports;
namespace Jivy_Malik_Goods_Transport_Company.ViewModel.Admin.BuiltyData
{
    public class BuiltyCrudViewModel
    {
        string connection = ConfigurationManager.ConnectionStrings["MalikDb"].ConnectionString;

        public List<Builty> GetAllBuilty()
        {
            var builtylist = new List<Builty>();

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Builty_GetAllBuilty", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    var builty = new Builty();
                    builty.BuiltyNumber = int.Parse(dr["BuiltyNumber"].ToString());
                    builty.BuiltyTruckNumber = dr["BuiltyTruckNumber"].ToString();
                    builty.BuiltyDate = dr["BuiltyDate"].ToString();
                    builty.BuiltyDriver = dr["BuiltyDriver"].ToString();
                    builty.BuiltySource = dr["BuiltySource"].ToString();
                    builty.BuiltyDestination = dr["BuiltyDestination"].ToString();
                    builty.BuiltySeller = dr["BuiltySender"].ToString();
                    builty.BuiltyBuyer = dr["BuiltyReciever"].ToString();
                    builty.BuiltyQuantity = dr["BuiltyQuantity"].ToString();
                    builty.BuiltyGoodsDetail = dr["BuiltyGoodsDetail"].ToString();
                    builty.BuiltyWeight = dr["BuiltyWeight"].ToString();
                    builty.BuiltyFare = int.Parse(dr["BuiltyFare"].ToString());
                    builty.BuiltyAdvance = int.Parse(dr["BuiltyAdvance"].ToString());
                    builty.BuiltyRemainingPayment = int.Parse(dr["BuiltyRemainingPayment"].ToString());
                    builty.BuiltyDriverNumber = dr["BuiltyDriverNumber"].ToString();
                    builty.BuiltyDriverId = dr["BuiltyDriverId"].ToString();
                    builty.BuiltyRate = int.Parse(dr["BuiltyRate"].ToString());
                    builty.BuiltyFinalFare = int.Parse(dr["BuiltyFinalFare"].ToString());
                    builty.BuiltyType = dr["BuiltyType"].ToString();
                    builtylist.Add(builty);
                }
            }
            con.Close();

            return builtylist;
        }





        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> vehiclelist = new List<Vehicle>();
            string connection = ConfigurationManager.ConnectionStrings["MalikDb"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("select VehicleNumber from tbl_Vehicle", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Vehicle vehicle = new Vehicle();
                    vehicle.VehicleNumber = sdr["VehicleNumber"].ToString();
                    vehiclelist.Add(vehicle);
                }
            }
            con.Close();
            return vehiclelist;
        }

        public Vehicle GetVehicleInformation(string VehicleNumber)
        {
            var VehicleData = new Vehicle();
            string connection = ConfigurationManager.ConnectionStrings["MalikDb"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            if(VehicleNumber!=null)
            {
                VehicleNumber = VehicleNumber.ToUpper();
            }

            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tbl_Vehicle where VehicleNumber='" + VehicleNumber + "'", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                if (sdr.Read())
                {
                    VehicleData.VehicleNumber = sdr["VehicleNumber"].ToString();
                    VehicleData.VehicleDriver = sdr["VehicleDriver"].ToString();
                    VehicleData.VehiclePhoneNumber = sdr["VehiclePhoneNumber"].ToString();
                    VehicleData.VehicleSize = sdr["VehicleSize"].ToString();
                    VehicleData.VehicleDriverCNIC = sdr["VehicleDriverCNIC"].ToString();
                }
            }
            con.Close();
            return VehicleData;

        }

        public DataSet SaveBuilty(Builty builty)
        {
            int BuiltyId = 0;
            DataSet ds = new FinalBuiltyDataSet();
            string connection = ConfigurationManager.ConnectionStrings["MalikDb"].ConnectionString;
            if(builty.BuiltyTruckNumber!=null)
            {
                builty.BuiltyTruckNumber = builty.BuiltyTruckNumber.ToUpper();
            }

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd0 = new SqlCommand("sp_Vehicle_VehicleExist", con);
            cmd0.CommandType = CommandType.StoredProcedure;
            cmd0.Parameters.AddWithValue("@VehicleNumber", builty.BuiltyTruckNumber);
            SqlDataReader sdr = cmd0.ExecuteReader();
            if (sdr.HasRows)
            {

                if(sdr.Read())
                {
                    var VehicleInDB = new Vehicle();
                    VehicleInDB.VehicleDriver=sdr["VehicleDriver"].ToString();
                    VehicleInDB.VehicleDriverCNIC=sdr["VehicleDriverCNIC"].ToString();
                    VehicleInDB.VehiclePhoneNumber=sdr["VehiclePhoneNumber"].ToString();
                    VehicleInDB.VehicleSize=sdr["VehicleSize"].ToString();
                    if ( VehicleInDB.VehicleDriver!= builty.BuiltyDriver ||  VehicleInDB.VehicleDriverCNIC!= builty.BuiltyDriverId ||  VehicleInDB.VehiclePhoneNumber!= builty.BuiltyDriverNumber || VehicleInDB.VehicleSize!=builty.BuiltyVehicleSize)
                    {
                        SqlCommand cmd1 = new SqlCommand("sp_Vehicle_VehicleUpdate", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@VehicleNumber", builty.BuiltyTruckNumber);
                        cmd1.Parameters.AddWithValue("@DriverName", builty.BuiltyDriver);
                        cmd1.Parameters.AddWithValue("@DriverPhoneNumber", builty.BuiltyDriverNumber);
                        cmd1.Parameters.AddWithValue("@VehicleDriverCNIC", builty.BuiltyDriverId);
                        cmd1.Parameters.AddWithValue("@VehicleSize", builty.BuiltyVehicleSize);
                        cmd1.ExecuteNonQuery();
                    }
                }

            }
            else if(builty.BuiltyTruckNumber!=null)
            {
                SqlCommand cmd1 = new SqlCommand("sp_Vehicle_VehicleAdd", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@VehicleNumber", builty.BuiltyTruckNumber);
                cmd1.Parameters.AddWithValue("@DriverName", builty.BuiltyDriver);
                cmd1.Parameters.AddWithValue("@DriverPhoneNumber", builty.BuiltyDriverNumber);
                cmd1.Parameters.AddWithValue("@VehicleDriverCNIC", builty.BuiltyDriverId);
                cmd1.Parameters.AddWithValue("@VehicleSize", builty.BuiltyVehicleSize);
                cmd1.ExecuteNonQuery();
            }
            builty.BuiltyRemainingPayment = builty.BuiltyFare+builty.BuiltyAdvance;
            builty.BuiltyFinalFare = builty.BuiltyRemainingPayment;
            SqlCommand cmd = new SqlCommand("sp_NewBuilty", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BuiltyVehicleNumber", builty.BuiltyTruckNumber);
            cmd.Parameters.AddWithValue("@BuiltyDate", builty.BuiltyDate);
            cmd.Parameters.AddWithValue("@BuiltyDriverName", builty.BuiltyDriver);
            cmd.Parameters.AddWithValue("@BuiltySource", builty.BuiltySource);
            cmd.Parameters.AddWithValue("@BuiltyDestination", builty.BuiltyDestination);
            cmd.Parameters.AddWithValue("@BuiltySellerName", builty.BuiltySeller);
            cmd.Parameters.AddWithValue("@BuiltyBuyerName", builty.BuiltyBuyer);
            cmd.Parameters.AddWithValue("@Quantity", builty.BuiltyQuantity);
            cmd.Parameters.AddWithValue("@BuiltyDetail", builty.BuiltyGoodsDetail);
            cmd.Parameters.AddWithValue("@BuiltyWeight", builty.BuiltyWeight);
            cmd.Parameters.AddWithValue("@BuiltyFare", builty.BuiltyFare);
            cmd.Parameters.AddWithValue("@BuiltyAdvance", builty.BuiltyAdvance);
            cmd.Parameters.AddWithValue("@BuiltyRemainingPayment", builty.BuiltyRemainingPayment);
            cmd.Parameters.AddWithValue("@BuiltyDriverNumber", builty.BuiltyDriverNumber);
            cmd.Parameters.AddWithValue("@BuiltyDriverId", builty.BuiltyDriverId);
            cmd.Parameters.AddWithValue("@BuiltyRate", builty.BuiltyRate);
            cmd.Parameters.AddWithValue("@BuiltyFinalFare", builty.BuiltyFinalFare);
            cmd.Parameters.AddWithValue("@BuiltyType", builty.BuiltyType);
            cmd.Parameters.AddWithValue("@BuiltyVehicleSize", builty.BuiltyVehicleSize);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable DT = new DataTable();
            SDA.Fill(DT);
            if (DT.Rows.Count > 0)
            { 
                foreach(DataRow Row in DT.Rows)
                {
                    BuiltyId = int.Parse(Row["BuiltyNumber"].ToString());
                }
            }

           
                con.Close();
               cmd.Dispose();
                using (SqlCommand cm = new SqlCommand("sp_Builty_Report", con))
                {
                    cm.CommandType = System.Data.CommandType.StoredProcedure;


                    
                    cm.Parameters.AddWithValue("@BuiltyNumber",BuiltyId);

                    SqlDataAdapter sda = new SqlDataAdapter(cm);
                    

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {

                            string firstname = r["BuiltySource"].ToString();
                            string lastname = r["BuiltyDriver"].ToString();
                           

                        
                        }
                    }


                   sda.Fill(ds, "tbl_Builty");

                    }
              

            

            return ds;
        }


        public DataSet BuiltyUpdate(Builty builty)
        {
            if (builty.BuiltyTruckNumber != null)
            {
                builty.BuiltyTruckNumber = builty.BuiltyTruckNumber.ToUpper();
            }
            string connection = ConfigurationManager.ConnectionStrings["MalikDb"].ConnectionString;

            SqlConnection con = new SqlConnection(connection);
            DataSet ds = new FinalBuiltyDataSet();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Builty_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BuiltyNumber", builty.BuiltyNumber);
            cmd.Parameters.AddWithValue("@BuiltyVehicleNumber", builty.BuiltyTruckNumber);
            cmd.Parameters.AddWithValue("@BuiltyDate", builty.BuiltyDate);
            cmd.Parameters.AddWithValue("@BuiltyDriverName", builty.BuiltyDriver);
            cmd.Parameters.AddWithValue("@BuiltySource", builty.BuiltySource);
            cmd.Parameters.AddWithValue("@BuiltyDestination", builty.BuiltyDestination);
            cmd.Parameters.AddWithValue("@BuiltySellerName", builty.BuiltySeller);
            cmd.Parameters.AddWithValue("@BuiltyBuyerName", builty.BuiltyBuyer);
            cmd.Parameters.AddWithValue("@Quantity", builty.BuiltyQuantity);
            cmd.Parameters.AddWithValue("@BuiltyDetail", builty.BuiltyGoodsDetail);
            cmd.Parameters.AddWithValue("@BuiltyWeight", builty.BuiltyWeight);
            cmd.Parameters.AddWithValue("@BuiltyFare", builty.BuiltyFare);
            cmd.Parameters.AddWithValue("@BuiltyAdvance", builty.BuiltyAdvance);
            cmd.Parameters.AddWithValue("@BuiltyRemainingPayment", builty.BuiltyRemainingPayment);
            cmd.Parameters.AddWithValue("@BuiltyDriverNumber", builty.BuiltyDriverNumber);
            cmd.Parameters.AddWithValue("@BuiltyDriverId", builty.BuiltyDriverId);
            cmd.Parameters.AddWithValue("@BuiltyRate", builty.BuiltyRate);
            cmd.Parameters.AddWithValue("@BuiltyFinalFare", builty.BuiltyFinalFare);
            cmd.Parameters.AddWithValue("@BuiltyType", builty.BuiltyType);
            cmd.Parameters.AddWithValue("@BuiltyVehicleSize", builty.BuiltyVehicleSize);
            string message = "";
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                message = "Ride Updated Successfully";

            }
            con.Close();
            using (SqlCommand cm = new SqlCommand("sp_Builty_Report", con))
            {
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@BuiltyNumber", builty.BuiltyNumber);
                SqlDataAdapter sda = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        string firstname = r["BuiltyDriverNumber"].ToString();
                    }
                }
                sda.Fill(ds, "tbl_Builty");
            }
            return ds;
        }

        public Builty Editbuilty(int id)
        {
            Builty builty = new Builty();
            string connection = ConfigurationManager.ConnectionStrings["MalikDb"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Builty_Select", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BuiltyId ", id);


            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    builty.BuiltyNumber = int.Parse(sdr["BuiltyNumber"].ToString());
                    builty.BuiltyTruckNumber = sdr["BuiltyTruckNumber"].ToString();
                    builty.BuiltyDate = sdr["BuiltyDate"].ToString();
                    builty.BuiltyDriver = sdr["BuiltyDriver"].ToString();
                    builty.BuiltySource = sdr["BuiltySource"].ToString();
                    builty.BuiltyDestination = sdr["BuiltyDestination"].ToString();
                    builty.BuiltySeller = sdr["BuiltySender"].ToString();
                    builty.BuiltyBuyer = sdr["BuiltyReciever"].ToString();
                    builty.BuiltyQuantity = sdr["BuiltyQuantity"].ToString();
                    builty.BuiltyGoodsDetail = sdr["BuiltyGoodsDetail"].ToString();
                    builty.BuiltyWeight = sdr["BuiltyWeight"].ToString();
                    builty.BuiltyFare = int.Parse(sdr["BuiltyFare"].ToString());
                    builty.BuiltyAdvance = int.Parse(sdr["BuiltyAdvance"].ToString());
                    builty.BuiltyRemainingPayment = int.Parse(sdr["BuiltyRemainingPayment"].ToString());
                    builty.BuiltyDriverNumber = sdr["BuiltyDriverNumber"].ToString();
                    builty.BuiltyDriverId = sdr["BuiltyDriverId"].ToString();
                    builty.BuiltyRate = int.Parse(sdr["BuiltyRate"].ToString());
                    builty.BuiltyFinalFare = int.Parse(sdr["BuiltyFinalFare"].ToString());
                    builty.BuiltyType = sdr["BuiltyType"].ToString();
                    builty.BuiltyVehicleSize = sdr["BuiltyVehicleSize"].ToString();
                }
            }
            con.Close();
            return builty;
        }

        
        public string BuiltyDelete(string id)
        {

            string message = "";
            string connection = ConfigurationManager.ConnectionStrings["MalikDb"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Delete_Builty", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BuiltyId", id);

            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                message = "1";
            }
            else if (cmd.ExecuteNonQuery() == 0)
            {
                message = "0";
            }
            else
            {
                throw new Exception("Problem During Delete");
            }

            con.Close();
            return message;
        }
    }
}