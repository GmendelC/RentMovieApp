using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using Unity.Container;
using Models;
using Models.Infra;
using DAL;
using DAL.MovieRentContext;
using RentMovieApp.Models;

namespace RentMovieApp.App_Start
{
    public class RentalMovieIoCIntregator
    {
        IUnityContainer _IoCIntregator;
        public RentalMovieIoCIntregator()
        {
            _IoCIntregator = new UnityContainer();
            _IoCIntregator.RegisterInstance<string>("");
            _IoCIntregator.RegisterType<ILoggerStringService, FileStringLogger>();
            _IoCIntregator.RegisterType<IRentalDBService, RentalMovieContext>();
            _IoCIntregator.RegisterType<IUserAutenticationService, MvcUserAutentication>();
            _IoCIntregator.RegisterType<IManagerRentalDB, DALManager>();
            _IoCIntregator.RegisterType<MovieReprosityModel>();
        }

        public IUserAutenticationService UserAutenticationService
        {
            get { return _IoCIntregator.Resolve<IUserAutenticationService>(); }
        }
        public IManagerRentalDB ManagerRentalDB
        {
            get { return _IoCIntregator.Resolve<IManagerRentalDB>(); }
        }
        public IUnityContainer IoCRentMovie
        {
            get { return _IoCIntregator; }
        }
        public MovieReprosityModel MvcMovieReprosityModel
        {
            get { return _IoCIntregator.Resolve<MovieReprosityModel>(); }
        }
    }
}