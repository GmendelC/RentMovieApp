using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Infra
{
    public interface IRentalDBService : IDisposable
    {
        // dispose because the db connection dispose

        // service of db for rental movie
        // have to query entitys and crud operation.
        IEnumerable<User> Users { get; }
        IEnumerable<Movie> Movies { get;  }
        IEnumerable<MovieCopy> Copies { get;  }
        IEnumerable<RentHistory> RentHistory { get; }

        T Create<T>(T newEntity) where T : class;
        T Update<T>(T updateEntity) where T : class;
        T Delete<T>(T delEntity) where T : class;
    }
}
