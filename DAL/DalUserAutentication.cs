using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Infra;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Diagnostics;

namespace DAL
{
    public class DalUserAutentication : IUserAutenticationService
    {
        // To autenticate user in db
        // Is base class Inherit it to implement the autentication
        // also for your app
        protected IRentalDBService _dbService;
        public DalUserAutentication(IRentalDBService dbServide)
        {
            _dbService = dbServide;
        }
        public virtual bool CreateUser(User newUser)
        {
            if (newUser!= null && UserVerification(newUser) && UserNotCreate(newUser))
            {
                HashUser(newUser);
                try
                {
                    _dbService.Create(newUser);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return false;
                }
                Login(ref newUser);
                return true;
            }
            else
                return false;
        }


        private bool BaseUserLogin(ref User user)
        {
            if (user == null|| user.Password == null || user.Email == null)
                return false;

            var email = user.Email;

            User dbUser = _dbService.Users
                .FirstOrDefault(u => u.Email == email);
            if (dbUser != null)
            {
                HashUser(user);
                bool res = user.Password == dbUser.Password;
                user.ID = dbUser.ID;
                user = dbUser;
                return res;
            }
            else
                return false;
        }

        public virtual bool Login(ref User user)
        {
            return BaseUserLogin(ref user);
        }

        public virtual bool Logout(User user)
        {
            return true;
        }

        public virtual bool UpdateUser(User updateUser)
        {
            if (updateUser == null)
                return false;
            string email = _dbService.Users
                .FirstOrDefault(u => u.ID == updateUser.ID).Email;
            User oldUser = new User { Email =email, Password = updateUser.OldPassord};

            if (Login(ref oldUser) && UserVerification(updateUser))
            {
                HashUser(updateUser);
                updateUser = UpadateUserNoContex(updateUser, oldUser);

                _dbService.Update(updateUser);
                return true;
            }
            else
                return false;
        }

        private static User UpadateUserNoContex(User updateUser, User oldUser)
        {
            oldUser.Email = updateUser.Email;
            oldUser.Password = updateUser.Password;

            updateUser = oldUser;
            return updateUser;
        }

        protected virtual void HashUser(User newUser)
        {
            SHA256Managed hashService = new SHA256Managed();
            UnicodeEncoding UE = new UnicodeEncoding();

            var passwordArray = UE.GetBytes(newUser.Password);
            var hashArray = hashService.ComputeHash(passwordArray);

            var hashString = UE.GetString(hashArray);
            newUser.Password = hashString;
        }

        protected virtual bool UserNotCreate(User newUser)
        {
            return !_dbService.Users
                .Any(u => u.Email == newUser.Email);
        }

        protected virtual bool UserVerification(User newUser)
        {
            if (newUser.Email == null || newUser.Password == null)
                return false;

            bool samePassWord = newUser.Password == newUser.PasswordConfirm;
            bool isEmail = Regex.IsMatch(newUser.Email, @"(@)(.+)$");

            return samePassWord && isEmail;
        }

        public void Dispose()
        {
            _dbService.Dispose();
        }
    }
}
