using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PRactice
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            //routes.MapRoute(
            //    name: "Account",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Rentals",
            //    url: "{controller}/{action}/{EditingUserId}",
            //    defaults: new {controller = "Home", action = "Index" }
                
            //    );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
