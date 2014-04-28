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
            routes.MapPageRoute("Index", "", "~/Pages/Index.aspx");
            routes.MapPageRoute("IndexEditRoute", "kund/{KundID}", "~/Pages/IndexEdit.aspx");
            routes.MapPageRoute("AlbumEditRoute", "album/{AlbumID}", "~/Pages/AlbumEdit.aspx");
            routes.MapPageRoute("Album", "album", "~/Pages/Album.aspx");
            
        }

    }
}