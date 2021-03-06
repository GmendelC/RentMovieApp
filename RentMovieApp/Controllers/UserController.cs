﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentMovieApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            User user = new User();
            return View(user);
        }

        // GET: User/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            using (var userAutentication = MvcApplication.APP_IOC.UserAutenticationService)
                if (userAutentication.Login(ref user))
                    return RedirectToAction("Index", "Home");
                else
                {
                    user.Password = null;
                    return View(user);
                }
        }

        // GET: User/Create
        public ActionResult Register()
        {
            User user = new User();
            return View(user);
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            using (var userAutentication = MvcApplication.APP_IOC.UserAutenticationService)
                if (userAutentication.CreateUser(user))
                    return RedirectToAction("Index", "Home");
                else
                {
                    if (ModelState.IsValid)
                        ModelState.AddModelError("Email", "It mail was register");
                    user.Password = null;
                    user.PasswordConfirm = null;
                    return View(user);
                }
        }

        // GET: User/Edit/5
        [Authorize]
        public ActionResult Edit()
        {
            User user;
            using (var dbManager = MvcApplication.APP_IOC.ManagerRentalDB)
                user = dbManager.User.FirstOrDefault(u => u.ID.ToString() == User.Identity.Name);
            if (user != null)
            {
                user.Password = null;
                return View(user);
            }
            else
                return RedirectToAction("LogOut");
        }

        // POST: User/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            using (var userAutentication = MvcApplication.APP_IOC.UserAutenticationService)
                if (userAutentication.UpdateUser(user))
                    return RedirectToAction("Index", "Home");
                else
                {
                    if (ModelState.IsValid)
                        ModelState.AddModelError("Email", "Passowrd not valid");
                    user.Password = null;
                    user.PasswordConfirm = null;
                    user.OldPassord = null;
                    return View(user);
                }
        }

        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            using (var userAutentication = MvcApplication.APP_IOC.UserAutenticationService)
                userAutentication.Logout(null);
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        public ActionResult AjaxLogOut()
        {
            using (var userAutentication = MvcApplication.APP_IOC.UserAutenticationService)
                userAutentication.Logout(null);
            return PartialView("_LoginPartial");
        }

        public ContentResult IsEmptyEmail(string email)
        {
            using (var dbManager = MvcApplication.APP_IOC.ManagerRentalDB)
            {

                bool res = !dbManager.User.Any(u => u.Email == email);
                return Content(res.ToString());
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
