using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentMovieApp.Controllers.Home
{
    [Authorize]
    public class MovieCopyController : Controller
    {
        // GET: MovieCopy/Details/5
        public ActionResult Details(int id)
        {
            MovieCopy copy;
            using (var managerRentalDB = MvcApplication.APP_IOC.ManagerRentalDB)
            {
                copy = managerRentalDB.Copies.FirstOrDefault(c => c.Id == id);
            }
            return View(copy);
        }

        
        // Id movie of copy
        public ActionResult Create(int id)
        {
            using (var managerRentalDB = MvcApplication.APP_IOC.ManagerRentalDB)
            {
                Movie movie = managerRentalDB.Movies.FirstOrDefault(c => c.Id == id);
                MovieCopy copy = new MovieCopy { ForMovie = movie };
                managerRentalDB.AddCopy(copy);
            }
            return RedirectToAction("Details","Movie",new {id = id});
        }

        
        // GET: MovieCopy/Delete/5
        public ActionResult Delete(int id)
        {
            MovieCopy copy;
            using (var managerRentalDB = MvcApplication.APP_IOC.ManagerRentalDB)
            {
                copy = managerRentalDB.Copies.FirstOrDefault(c => c.Id == id);
            }
            return View(copy);
        }
        
        // POST: MovieCopy/Delete/5
        public ActionResult DeleteAction(int id)
        {
            MovieCopy copy;
            using (var managerRentalDB = MvcApplication.APP_IOC.ManagerRentalDB)
            {
                copy = managerRentalDB.Copies.FirstOrDefault(c => c.Id == id);
                managerRentalDB.RemoveCopy(copy);
            }
            return RedirectToAction("Details", "Movie", new { id = copy.ForMovie.Id });
        }

        public ActionResult Rent(int id)
        {
            MovieCopy copy;
            using (var managerRentalDB = MvcApplication.APP_IOC.ManagerRentalDB)
            {
                copy = managerRentalDB.Copies.FirstOrDefault(c => c.Id == id);
            }
            return View(copy);
        }

        [HttpPost]
        public ActionResult Rent(int id, DateTime returnDate)
        {
            MovieCopy copy;
            using (var managerRentalDB = MvcApplication.APP_IOC.ManagerRentalDB)
            {
                copy = managerRentalDB.Copies.FirstOrDefault(c => c.Id == id);
                copy.ReturnDate = returnDate;
                var user = managerRentalDB.User.FirstOrDefault(u => u.ID.ToString() == User.Identity.Name);
                managerRentalDB.RentCopy(copy, user);
            }
            return RedirectToAction("Details", "Movie", new { id = copy.ForMovie.Id });
        }

        public ActionResult Return(int id)
        {
            MovieCopy copy;
            using (var managerRentalDB = MvcApplication.APP_IOC.ManagerRentalDB)
            {
                copy = managerRentalDB.Copies.FirstOrDefault(c => c.Id == id);
                managerRentalDB.RenturnCopy(copy);
            }
            return RedirectToAction("Details", "Movie", new { id = copy.ForMovie.Id });
        }
        //[HttpPost]
        public PartialViewResult AjaxReturn(int id)
        {
            MovieCopy copy;
            MovieCopy[] copies;
            using (var managerRentalDB = MvcApplication.APP_IOC.ManagerRentalDB)
            {
                copy = managerRentalDB.Copies.FirstOrDefault(c => c.Id == id);
                managerRentalDB.RenturnCopy(copy);
                copies = managerRentalDB.Copies
                    .Where(c => c.ForMovieId == copy.ForMovieId).ToArray();
            }
            return PartialView("_CopyPartial", copies);
        }
    }
}
