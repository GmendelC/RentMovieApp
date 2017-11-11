using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentMovieApp.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            Movie movie;
            using (var managerRentalDB = MvcApplication.APP_IOC.ManagerRentalDB)
            {
                movie = managerRentalDB.Movies.FirstOrDefault(m => m.Id == id);
            }
            return View(movie);
        }

        // GET: Movie/Create
        [Authorize]
        public ActionResult Create()
        {
            Movie newMovie = new Movie();
            return View(newMovie);
        }

        // POST: Movie/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie, HttpPostedFileBase movieImage)
        {
            using (var movieReprosity = MvcApplication.APP_IOC
                .MvcMovieReprosityModel)
                if (ModelState.IsValid && 
                    movieReprosity.CreateMovie(movie, movieImage))
                return RedirectToAction("Index");
            else
                return View(movie);
        }

        // GET: Movie/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {

            Movie movie;
            using (var managerRentalDB = MvcApplication.APP_IOC.ManagerRentalDB)
            {
                movie = managerRentalDB.Movies.FirstOrDefault(m => m.Id == id);
            }
            if (movie != null)
                return View(movie);
            else
                return RedirectToAction("Index");
        }

        // POST: Movie/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie, HttpPostedFileBase movieImage)
        {
            using( var movieReprosity = MvcApplication.APP_IOC
                .MvcMovieReprosityModel)
             if (ModelState.IsValid && 
                    movieReprosity.UpdateMovie(movie, movieImage))
                return RedirectToAction("Index");
            else
                return View(movie);
        }

        public FileContentResult GetMovieImage(int id)
        {
            using (var movieReprosity = MvcApplication.APP_IOC
                .MvcMovieReprosityModel)
                return movieReprosity.GetMovieImage(id);
        }
    }
}
