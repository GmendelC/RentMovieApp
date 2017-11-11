using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Infra;
using Models;
using System.IO;
using System.Web.Mvc;

namespace RentMovieApp.Models
{
    public class MovieReprosityModel : IDisposable
    {
        private IManagerRentalDB _dbManager;

        public MovieReprosityModel(IManagerRentalDB dbManager)
        {
            _dbManager = dbManager;
        }

        public bool CreateMovie(Movie newMovie, HttpPostedFileBase httpFile)
        {
            if (EnterFileInMovieModel(newMovie, httpFile))
                return _dbManager.AddMovie(newMovie);
            else
                return false;
        }

        public bool UpdateMovie(Movie updateMovie, HttpPostedFileBase httpFile)
        {
            if(httpFile == null && updateMovie != null)
            {
                SetDbMovieImage(updateMovie);
                return _dbManager.UpdateMovie(updateMovie);
            }
            if (EnterFileInMovieModel(updateMovie, httpFile))
                return _dbManager.UpdateMovie(updateMovie);
            else
                return false;
        }

        private void SetDbMovieImage(Movie updateMovie)
        {
            Movie oldMovie = _dbManager.Movies
                                .FirstOrDefault(m => m.Id == updateMovie.Id);
            if (oldMovie != null)
            {
                oldMovie.Category = updateMovie.Category;
                oldMovie.Copies = updateMovie.Copies;
                oldMovie.Description = updateMovie.Description;
                oldMovie.MovieName = updateMovie.MovieName;
                oldMovie.Price = updateMovie.Price;
                updateMovie = oldMovie;
            }
        }

        private bool EnterFileInMovieModel(Movie newMovie, HttpPostedFileBase httpFile)
        {
            if (newMovie != null && httpFile != null && httpFile.ContentType.Contains("image"))
            {
                newMovie.ImageMime = httpFile.ContentType;
                newMovie.ImageBits = new byte[httpFile.ContentLength];
                httpFile.InputStream.Read(newMovie.ImageBits, 0, httpFile.ContentLength);
                return true;
            }
            else
                return false;
        }

        public FileContentResult GetMovieImage(int movieId)
        {
            var movie = _dbManager.Movies.FirstOrDefault(m => m.Id == movieId);
            if (movie.ImageBits != null & movie.ImageMime != null)
                return new FileContentResult(movie.ImageBits, movie.ImageMime);
            else
                return null;
        }

        public void Dispose()
        {
            _dbManager.Dispose();
        }
    }
}