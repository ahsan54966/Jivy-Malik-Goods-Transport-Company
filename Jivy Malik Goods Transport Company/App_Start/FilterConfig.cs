﻿using System.Web;
using System.Web.Mvc;

namespace Jivy_Malik_Goods_Transport_Company
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
