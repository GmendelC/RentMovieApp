using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Infra;
using System.Web;
using System.Diagnostics;

namespace RentMovieApp.Tests.Doubles
{
    public class MockRentalDb : IRentalDBService
    {
        private SetMap _map = new SetMap();

        public IEnumerable<User> Users
        {
            get => _map.Get<User>();
            set => _map.Use<User>(value);
        }

        public IEnumerable<Movie> Movies
        {
            get => _map.Get<Movie>();
            set => _map.Use<Movie>(value);
        }

        public IEnumerable<MovieCopy> Copies
        {
            get => _map.Get<MovieCopy>();
            set => _map.Use<MovieCopy>(value);
        }

        public IEnumerable<RentHistory> RentHistory
        {
            get => _map.Get<RentHistory>();
            set => _map.Use<RentHistory>(value);
        }

        public T Create<T>(T newEntity) where T : class
        {
            _map.Get<T>().Add(newEntity);
            return newEntity; 
        }

        public T Delete<T>(T delEntity) where T : class
        {
            _map.Get<T>().Remove(delEntity);
            return delEntity;
        }

        public void Dispose()
        {
            
        }

        public T Update<T>(T updateEntity) where T : class
        {
            Debug.WriteLine("No Upadate in doble, change the mock object");
            return updateEntity;
        }
    }
}
