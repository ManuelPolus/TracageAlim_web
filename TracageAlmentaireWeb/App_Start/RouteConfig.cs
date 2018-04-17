using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace TracageAlmentaireWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //___HOME CONTROLLER___

            routes.MapRoute(
                name: "HomeRoute",
                url: "home",
                defaults: new { controller = "Home", action = "Index" }
            );


            //___CONNECTION CONTROLLER___

            routes.MapRoute(
                name: "loginRoute",
                url: "",
                defaults: new { controller = "Connection", action = "LoginPage" }
            );

            routes.MapRoute(
                name: "loginRoutespecified",
                url: "login",
                defaults: new { controller = "Connection", action = "LoginPage" }
            );

            routes.MapRoute(
                name: "loginDoneRoute",
                url: "login/logged",
                defaults: new { controller = "Connection", action = "Login" }
            );

            routes.MapRoute(
                name: "SignInRoute",
                url: "login/signIn",
                defaults: new { controller = "Connection", action = "Register" }
            );

            //___PRODUCTS MANAGEMENT CONTROLLER

            routes.MapRoute(
                name: "productsListRoute",
                url: "products/list",
                defaults: new { controller = "ProductsManagement", action = "List" }
            );

            routes.MapRoute(
                name: "productsCreationRoute",
                url: "products/create",
                defaults: new { controller = "ProductsManagement", action = "Create" }
            );

            //___DEFAULT___

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

           

        }
    }
}
