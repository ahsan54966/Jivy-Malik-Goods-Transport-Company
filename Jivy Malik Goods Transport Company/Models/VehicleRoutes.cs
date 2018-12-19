using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jivy_Malik_Goods_Transport_Company.Models
{
    public class VehicleRoutes:Vehicle
    {


        public int RouteId { get; set; }

        public DateTime RouteDate { get; set; }

        public string RouteDestination { get; set; }

        public string RouteVehicleId { get; set; }




    }
}