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

            //___PROCESSES MANAGEMENT CONTROLLER

            routes.MapRoute(
                name: "processesListRoute",
                url: "processes/list",
                defaults: new { controller = "ProcessesManagement", action = "List" }
            );

            routes.MapRoute(
                name: "processesCreationRoute",
                url: "processes/create",
                defaults: new { controller = "ProcessesManagement", action = "Create" }
            );

            routes.MapRoute(
                name: "processesUpdateRoute",
                url: "processes/update",
                defaults: new { controller = "ProcessesManagement", action = "Update" }
            );

            routes.MapRoute(
                name: "processesDetailsRoute",
                url: "processes/details",
                defaults: new { controller = "ProcessesManagement", action = "Details" }
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

            routes.MapRoute(
                name: "productsUpdateRoute",
                url: "products/update",
                defaults: new { controller = "ProductsManagement", action = "Update" }
            );

            routes.MapRoute(
                name: "productsDetailsRoute",
                url: "products/details",
                defaults: new { controller = "ProductsManagement", action = "Details" }
            );

            //___TREATMENTS MANAGEMENT CONTROLLER

            routes.MapRoute(
                name: "treatmentsListRoute",
                url: "treatments/list",
                defaults: new { controller = "TreatmentsManagement", action = "List" }
            );

            routes.MapRoute(
                name: "treatmentsCreationRoute",
                url: "treatments/create",
                defaults: new { controller = "TreatmentsManagement", action = "Create" }
            );

            routes.MapRoute(
                name: "treatmentsUpdateRoute",
                url: "treatments/update",
                defaults: new { controller = "TreatmentsManagement", action = "Update" }
            );

            routes.MapRoute(
                name: "treatmentsDetailsRoute",
                url: "treatments/details",
                defaults: new { controller = "TreatmentsManagement", action = "Details" }
            );

            //___USERS MANAGEMENT CONTROLLER

            routes.MapRoute(
                name: "usersListRoute",
                url: "users/list",
                defaults: new { controller = "UsersManagement", action = "List" }
            );

            routes.MapRoute(
                name: "usersCreationRoute",
                url: "users/create",
                defaults: new { controller = "UsersManagement", action = "Create" }
            );

            routes.MapRoute(
                name: "usersUpdateRoute",
                url: "users/update",
                defaults: new { controller = "UsersManagement", action = "Update" }
            );

            routes.MapRoute(
                name: "usersDetailsRoute",
                url: "users/details",
                defaults: new { controller = "UsersManagement", action = "Details" }
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
