using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jivy_Malik_Goods_Transport_Company.Models
{
    public class Builty
    {
        public Builty()
        {
            BuiltyNumber = 0;
            BuiltyTruckNumber = "";
            BuiltyDate = "";
            BuiltyDriver="";
            BuiltySource="";
            BuiltyDestination="";
            BuiltySeller="";
            BuiltyBuyer="";
            BuiltyQuantity="";
            BuiltyGoodsDetail="";
            BuiltyWeight="";
            BuiltyFare=0;
            BuiltyAdvance=0;
            BuiltyRemainingPayment=0;
            BuiltyDriverNumber="";
            BuiltyDriverId="";
            BuiltyRate=0;
            BuiltyFinalFare=0;
            BuiltyType="";
            BuiltyVehicleSize="";

        }
        public int BuiltyNumber { get; set; }



        public string BuiltyTruckNumber { get; set; }

        public string BuiltyDate { get; set; }

        public string BuiltyDriver { get; set; }

        public string BuiltySource { get; set; }

        public string BuiltyDestination { get; set; }

        public string BuiltySeller { get; set; }

        public string BuiltyBuyer { get; set; }

        public string BuiltyQuantity { get; set; }

        public string BuiltyGoodsDetail { get; set; }

        public string BuiltyWeight { get; set; }

        public int BuiltyFare { get; set; }

        public int BuiltyAdvance { get; set; }

        public int BuiltyRemainingPayment { get; set; }

        public string BuiltyDriverNumber { get; set; }

        public string BuiltyDriverId { get; set; }

        public int? BuiltyRate { get; set; }
 
        public int? BuiltyFinalFare { get; set; }

        public string BuiltyType { get; set; }

        public string BuiltyVehicleSize { get; set; }
    }

}