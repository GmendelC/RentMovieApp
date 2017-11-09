using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Infra
{
    public interface IManagerRentalDB : IDisposable
    {
        // dispose because the db connection dispose
        IEnumerable<User> User { get;}
        IEnumerable<Movie> Movies { get; }
        IEnumerable<MovieCopy> Copies { get; }
        IEnumerable<RentHistory> RentHistory { get; }

        bool AddUser(User newUser);
        bool AddMovie(Movie newMovie);
        bool AddCopy(MovieCopy newCopy);

        bool UpdateUser(User updateUser);
        bool UpdateMovie(Movie updateMovie);

        bool RemoveCopy(MovieCopy removeCopy);

        bool RentCopy(MovieCopy rentCopy, User rentUser);
        bool RenturnCopy(MovieCopy rentCopy);
    }
}
