using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Infra;
using System.Diagnostics;

namespace DAL
{
    public class DALManager : IManagerRentalDB, IDisposable
    {
        private IRentalDBService _db;
        private IUserAutenticationService _AutenticationService;
        private ILoggerStringService _logger;

        public DALManager(IRentalDBService rentalDBService,
            IUserAutenticationService userAutenticationService,
            ILoggerStringService logger)
        {
            _db = rentalDBService;
            _AutenticationService = userAutenticationService;
            _logger = logger;
        }

        public IEnumerable<User> User => _db.Users;

        public IEnumerable<Movie> Movies => _db.Movies;

        public IEnumerable<MovieCopy> Copies => _db.Copies.Where(c => c!= null && !c.Removed);

        public IEnumerable<RentHistory> RentHistory => _db.RentHistory;

        public bool AddCopy(MovieCopy newCopy)
        {
            if (newCopy == null || (newCopy.ForMovie == null))
                return false;

            if (Movies.Any(m => m.Id == newCopy.ForMovie.Id))
            {
                return DbManipulation(newCopy, c =>
                {
                    _db.Create(c);
                    _logger.Log($"Copy {c.Id} create");
                    return true;
                });
            }
            else
                return false;
        }

        private TRes DbManipulation<T,TRes>(T entity, Func<T, TRes> func)
        {
            try
            {
                return func(entity);
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                return default(TRes);
            }
        }

        public bool AddMovie(Movie newMovie)
        {
            return DbManipulation(newMovie, m =>
           {
               _db.Create(m);
               _logger.Log($"Movie {m.Id} create");
               return true;
           });
        }

        public bool AddUser(User newUser)
        {
            return DbManipulation(newUser, u =>
            {
                _db.Create(u);
                _logger.Log($"User {u.ID} create");
                return true;
            });
        }

        public bool RemoveCopy(MovieCopy removeCopy)
        {
            removeCopy.Removed = true;
            return DbManipulation(removeCopy, c =>
            {
                _db.Update(c);
                _logger.Log($"Copy {c.Id} remove");
                return true;
            });
        }

        public bool RentCopy(MovieCopy rentCopy, User rentUser)
        {
            rentCopy.UserRent = rentUser;
            rentCopy.UserRentId = rentUser.ID;
            rentCopy.RentDate = DateTime.Now;

            return DbManipulation(rentCopy, c =>
            {
                _db.Update(c);
                _logger.Log($"Copy {c.Id} rent");
                return true;
            });
        }

        public bool RenturnCopy(MovieCopy rentCopy)
        {
            RentHistory rentHistory = new RentHistory { UserRent = rentCopy.UserRent, Copy = rentCopy };

            bool updateHistory = DbManipulation(rentHistory, h =>
            {
                _db.Create(h);
                return true;
            });

            rentHistory.RentDate = rentCopy.RentDate.Value;
            rentHistory.Copy = rentCopy;
            rentHistory.RentReturn = DateTime.Now;
            rentHistory.UserRent = rentCopy.UserRent;

            updateHistory = DbManipulation(rentHistory, h =>
            {
                _db.Update(h);
                return true;
            });

            rentCopy.RentDate = null;
            rentCopy.ReturnDate = null;
            rentCopy.UserRent = null;

            if (updateHistory)
            {
                bool updateCopy = DbManipulation(rentCopy, c =>
                {
                    _db.Update(c);
                    _logger.Log($"Copy {c.Id} return");
                    return true;
                });

                return updateCopy;
            }
            else
            {
                DbManipulation(rentHistory, h =>
                {
                    _db.Delete(h);
                    return false;
                });
                return false;
            }
        }

        public bool UpdateMovie(Movie updateMovie)
        {
            return DbManipulation(updateMovie, m =>
            {
                _db.Update(m);
                _logger.Log($"Movie {m.Id} update");
                return true;
            });
        }

        public bool UpdateUser(User updateUser)
        {
            return DbManipulation(updateUser, u =>
            {
                _db.Update(u);
                _logger.Log($"User {u.ID} update");
                return true;
            });
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
