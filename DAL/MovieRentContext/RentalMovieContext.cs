namespace DAL.MovieRentContext
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Models;
    using Models.Infra;

    public class RentalMovieContext : DbContext, IRentalDBService
    {
        // the asses of db ef contex 
        // Asses of db implement the IRentalDBServide interface

        public RentalMovieContext()
            : base("name=RentalMovieContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        IEnumerable<User> IRentalDBService.Users { get => Users; }

        public virtual DbSet<Movie> Movies { get; set; }
        IEnumerable<Movie> IRentalDBService.Movies { get => Movies; }

        public virtual DbSet<MovieCopy> Copies { get; set; }
        IEnumerable<MovieCopy> IRentalDBService.Copies { get => Copies; }

        public virtual DbSet<RentHistory> RentHistory { get; set; }
        IEnumerable<RentHistory>
            IRentalDBService.RentHistory { get => RentHistory; }

        public T Create<T>(T newEntity) where T : class
        {
            T res = Set<T>().Add(newEntity);
            SaveChanges();
            return res;
        }

        public T Delete<T>(T delEntity) where T : class
        {
            T res = Set<T>().Remove(delEntity);
            SaveChanges();
            return res;
        }

        public T Update<T>(T updateEntity) where T : class
        {
            var entry = Entry<T>(updateEntity);
            entry.State = EntityState.Modified;
            SaveChanges();
            return entry.Entity;
        }
    }

}