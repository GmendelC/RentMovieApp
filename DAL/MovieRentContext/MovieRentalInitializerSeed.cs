using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Models;
using Models.Enums;
using System.IO;
using System.Security.Cryptography;

namespace DAL.MovieRentContext
{
    public class MovieRentalInitializerSeed : MovieRentalInitializer
    {
        protected override void Seed(RentalMovieContext context)
        {
            // have to chage image path in change directory solution. 
            base.Seed(context);

            context.Movies.AddRange(new Movie[] {
                new Movie
                {
                    Category = EMovieCategory.Horror,
                    ImageBits = File.ReadAllBytes(@"C:\Users\CohenFamily\Desktop\RentMovieApp\DAL\Images\Cat.jpg"),
                    ImageMime = "image/jpg",
                    Description = "jfgjadgfjgdjfg jsdafgjagfj gjdfgjag jfjvfhgfhjhrgajhgba jak efddd",
                    MovieName = "Cat",
                    Price = 15.40
                },
                new Movie
                {
                    Category = EMovieCategory.Childern,
                    ImageBits = File.ReadAllBytes(@"C:\Users\CohenFamily\Desktop\RentMovieApp\DAL\Images\ChildCat.jpg"),
                    ImageMime = "image/jpg",
                    Description = "jfgjadgfjgdjfg jsdafgjagfj gjdfgjag jfjvfhgfhjhrgajhgba jak efddd",
                    MovieName = "ChildCat",
                    Price = 20.40
                },
                  new Movie
                {
                    Category = EMovieCategory.Commedy,
                    ImageBits = File.ReadAllBytes(@"C:\Users\CohenFamily\Desktop\RentMovieApp\DAL\Images\DogEnjoy.jpg"),
                    ImageMime = "image/jpg",
                    Description = "jfgjadgfjgdjfg jsdafgjagfj gjdfgjag jfjvfhgfhjhrgajhgba jak efddd",
                    MovieName = "DogEnjoy",
                    Price = 10.40
                },
                   new Movie
                {
                    Category = EMovieCategory.Action,
                    ImageBits = File.ReadAllBytes(@"C:\Users\CohenFamily\Desktop\RentMovieApp\DAL\Images\Dog.jpg"),
                    ImageMime = "image/jpg",
                    Description = "jfgjadgfjgdjfg jsdafgjagfj gjdfgjag jfjvfhgfhjhrgajhgba jak efddd",
                    MovieName = "Dog",
                    Price = 30.0
                }
            });

            context.SaveChanges();

            var usersArray = new User[]
            {
                new User{Email= "1@1.com", Password =HashString("1")},
                new User{Email= "adimstator@movie.com", Password =HashString("Password")},
                new User{Email= "employee@movie.com", Password =HashString("Password")}
            };

            context.Users.AddRange(usersArray);

            context.SaveChanges();

            foreach (var movie in context.Movies)
            {
                context.Copies.AddRange(new MovieCopy[]
                {
                    new MovieCopy{ ForMovie = movie, Removed = false, RentDate = DateTime.Now, ReturnDate = null, UserRent= usersArray[0]},
                    new MovieCopy{ ForMovie = movie, Removed = false, RentDate = null, ReturnDate = null},
                    new MovieCopy{ ForMovie = movie, Removed = false, RentDate = null, ReturnDate = null}
                });
            }
            context.SaveChanges();

            // seed some movie and copies.
        }

        static private string HashString(string passoword)
        {
            // to be in utilies static class
            SHA256Managed hashService = new SHA256Managed();
            UnicodeEncoding UE = new UnicodeEncoding();

            var passwordArray = UE.GetBytes(passoword);
            var hashArray = hashService.ComputeHash(passwordArray);

            var hashString = UE.GetString(hashArray);
            return hashString;
        }
    }
}
