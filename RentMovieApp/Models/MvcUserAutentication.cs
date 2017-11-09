using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using Models;
using Models.Infra;

namespace RentMovieApp.Models
{
    public class MvcUserAutentication : DalUserAutentication
    {
        public MvcUserAutentication(IRentalDBService rentalDBService) 
            : base(rentalDBService){}

        public override bool Login(User user)
        {
            if( base.Login(user))
        }
    }
}