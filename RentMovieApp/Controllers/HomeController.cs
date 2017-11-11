using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentMovieApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Movie> movies;
            using (var managerRentalDB = MvcApplication.APP_IOC.ManagerRentalDB)
            {
                movies = managerRentalDB.Movies.ToList();
            }
            return View(movies);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}