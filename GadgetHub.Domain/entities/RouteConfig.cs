﻿//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace GadgetHub.WebUI
//{
//    public class RouteConfig
//    {
//        public static void RegisterRoutes(RouteCollection routes)
//        {
//            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

//            routes.MapRoute(null, "", new
//            {
//                controller = "Product",
//                action = "List",
//                category = (string)null,
//                page = 1
//            });

//            routes.MapRoute(null, "Page{page}", new
//            {
//                controller = "Product",
//                action = "List",
//                category = (string)null,
//            },
//            new { page = @"\d+" }
//            );

//            routes.MapRoute(null, "{category}", new
//            {
//                controller = "Product",
//                action = "List",
//                page = 1
//            });

//            routes.MapRoute(null, "{category}/Page{page}", new
//            {
//                controller = "Product",
//                action = "List",
//            },
//            new { page = @"\d+" }
//            );

//            routes.MapRoute(null, "{controller}/{action}");

//            //routes.MapRoute(
//            //    name: null,
//            //    url: "Page{page}",
//            //    defaults: new
//            //    { Controller = "Product", action = "List" }
//            //);

//            //routes.MapRoute(
//            //    name: "Default",
//            //    url: "{controller}/{action}/{id}",
//            //    defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
//            //);
//        }
//    }
//}
