﻿using System;
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
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );


            //___CONNECTION CONTROLLER___

            routes.MapRoute(
                name: "loginRoute",
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

            //___DEFAULT___

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

           

        }
    }
}
