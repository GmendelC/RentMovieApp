namespace DAL.MovieRentContext
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Models;
    using Models.Infra;
    using System.Data.Entity.ModelConfiguration.Conventions;

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
        IEnumerable<Movie> IRentalDBService.Movies { get => Movies.Include(m => m.Copies) ; }

        public virtual DbSet<MovieCopy> Copies { get; set; }
        IEnumerable<MovieCopy> IRentalDBService.Copies { get => Copies.Include(c => c.ForMovie).Include(c=> c.UserRent); }

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieCopy>()
                .HasRequired(c => c.ForMovie)
                .WithMany(m => m.Copies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MovieCopy>()
                .HasOptional(c => c.UserRent)
                .WithMany(u => u.MovieCopiesRent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.MovieCopiesRent)
                .WithOptional(c => c.UserRent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Ignore(u => u.PasswordConfirm);

            modelBuilder.Entity<User>()
                .Ignore(u => u.OldPassord);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Copies)
                .WithRequired(c => c.ForMovie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RentHistory>()
                .HasRequired(h => h.Copy)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RentHistory>()
                .HasRequired(h => h.UserRent)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RentHistory>()
                .Property(h => h.RentDate)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);

            modelBuilder.Entity<RentHistory>()
                .Property(h => h.RentReturn)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);

            modelBuilder.Entity<RentHistory>()
                .Property(h => h.RentDate)
                .HasColumnType("datetime2")
                .HasPrecision(0);

            modelBuilder.Entity<RentHistory>()
                .Property(h => h.RentReturn)
                .HasColumnType("datetime2")
                .HasPrecision(0);
        }
    }

}