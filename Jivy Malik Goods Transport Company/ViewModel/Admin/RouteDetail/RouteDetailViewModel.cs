using Jivy_Malik_Goods_Transport_Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jivy_Malik_Goods_Transport_Company.ViewModel.Admin.RouteDetail
{
    public class RouteDetailViewModel
    {
        public VehicleRoutes Routes { get; set; }
        public List<VehicleRoutes> routedetail { get; set; }
    }
}