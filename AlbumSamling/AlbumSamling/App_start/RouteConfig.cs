using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace AlbumSamling.App_start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("IndexEditRoute", "Kund/ny", "AlbumSamling/Pages/IndexEdit.aspx");
            routes.MapPageRoute("Index", "Index", "~/Pages/Index.aspx");
            

        }

    }
}