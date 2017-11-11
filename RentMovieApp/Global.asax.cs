using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RentMovieApp.App_Start;
using System.Data.Entity;
using DAL.MovieRentContext;

namespace RentMovieApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static RentalMovieIoCIntregator APP_IOC;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<RentalMovieContext>(new MovieRentalInitializerSeed());
            // not worck
            //DAL.RelativePahtStarter.ChangeDataDirectoryToSolutionFolder();
            APP_IOC = new RentalMovieIoCIntregator();
        }
    }
}
