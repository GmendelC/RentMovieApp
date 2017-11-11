using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
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
            bool isRegister = base.Login(user);

            if (isRegister)
            {
                FormsAuthentication.SetAuthCookie(user.ID.ToString(), false);
            }

            return isRegister;
        }

        public override bool Logout(User user)
        {
            bool isLogout = base.Logout(user);

            if (isLogout)
            {
                FormsAuthentication.SignOut();
            }
            
            return isLogout;
        }
    }
}