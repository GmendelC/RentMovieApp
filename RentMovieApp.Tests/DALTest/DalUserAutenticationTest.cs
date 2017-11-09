using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Infra;
using DAL;
using RentMovieApp.Tests.Doubles;
using System.Security.Cryptography;

namespace RentMovieApp.Tests.DALTest
{
    /// <summary>
    /// Summary description for DalUserAutenticationTest
    /// </summary>
    [TestClass]
    public class DalUserAutenticationTest
    {
        static MockRentalDb _db = new MockRentalDb();

        static private string HashString(string passoword)
        {
            SHA256Managed hashService = new SHA256Managed();
            UnicodeEncoding UE = new UnicodeEncoding();

            var passwordArray = UE.GetBytes(passoword);
            var hashArray = hashService.ComputeHash(passwordArray);

            var hashString = UE.GetString(hashArray);
            return hashString;
        }

        public DalUserAutenticationTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
         [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            string password = HashString("testPassword");
            User[] mockUsers = {
                new User { Email = "admistator@test.com", Password = password },
                new User { Email = "employee@test.com", Password = password }
            };
            _db.Users = mockUsers;
        }

        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CreateUserTest()
        {
            var userAutenticationManager = new DalUserAutentication(_db);
            bool nullUser = userAutenticationManager.CreateUser(null);
            bool notvalidEmailUser = userAutenticationManager.CreateUser(
                new User { Email = "TestCreatetest.com", Password = "testPassword", PasswordConfirm = "testPassword" });
            bool notSamePasswordUser = userAutenticationManager.CreateUser(
                new User { Email = "TestCreate@test.com", Password = "testPassword", PasswordConfirm = "" });
            bool nullPasswordUser = userAutenticationManager.CreateUser(
               new User { Email = "TestCreate@test.com", Password = "", PasswordConfirm = "" });
            bool existUser = userAutenticationManager.CreateUser(
                new User { Email = "employee@test.com", Password = "testPassword", PasswordConfirm = "testPassword" });

            bool corretUser = userAutenticationManager.CreateUser(
                new User {Email = "TestCreate@test.com", Password= "testPassword", PasswordConfirm = "testPassword" });

            Assert.IsTrue(corretUser);
            Assert.IsFalse(nullUser);
            Assert.IsFalse(notvalidEmailUser);
            Assert.IsFalse(notSamePasswordUser);
            Assert.IsFalse(nullPasswordUser);
            Assert.IsFalse(existUser);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var userAutenticationManager = new DalUserAutentication(_db);
            bool nullUser = userAutenticationManager.UpdateUser(null);
            bool notExistUser = userAutenticationManager.UpdateUser(
                new User { Email = "TestUpdate@test.com", Password = "testPassword2", OldPassord = "testPassword" });
            bool notvalidEmailUser = userAutenticationManager.UpdateUser(
                new User { Email = "employee@test.com", Password = "testPassword2", OldPassord = "testPassword" });
            bool nullPasswordUser = userAutenticationManager.UpdateUser(
               new User { Email = "employee@test.com", Password = "", OldPassord = "testPassword" });
            bool corretUser = userAutenticationManager.UpdateUser(
                new User { Email = "employee@test.com", Password = "testPassword2", OldPassord = "testPassword" });
            bool nullOldPassword = userAutenticationManager.UpdateUser(
               new User { Email = "employee@test.com", Password = "testPassword2", OldPassord = "" });


            Assert.IsTrue(corretUser);
            Assert.IsFalse(nullUser);
            Assert.IsFalse(notvalidEmailUser);
            Assert.IsFalse(notExistUser);
            Assert.IsFalse(nullPasswordUser);
            Assert.IsFalse(nullOldPassword);
        }

        [TestMethod]
        public void LoginTest()
        {
            var userAutenticationManager = new DalUserAutentication(_db);
            bool nullUser = userAutenticationManager.Login(null);
            bool corretUser = userAutenticationManager.Login(
                new User { Email = "admistator@test.com", Password = "testPassword" });
            bool notvalidEmailUser = userAutenticationManager.Login(
                new User { Email = "admistatortest.com", Password = "testPassword"});
            bool notExistUser = userAutenticationManager.Login(
                new User { Email = "TestCreate@test.com", Password = "testPassword" });
            bool nullPasswordUser = userAutenticationManager.Login(
               new User { Email = "admistator@test.com", Password = ""});
            bool notValidPasswordUser = userAutenticationManager.Login(
               new User { Email = "admistator@test.com", Password = "jjkfghksd" });

            Assert.IsTrue(corretUser);
            Assert.IsFalse(nullUser);
            Assert.IsFalse(notvalidEmailUser);
            Assert.IsFalse(notValidPasswordUser);
            Assert.IsFalse(nullPasswordUser);
            Assert.IsFalse(notExistUser);
        }

        [TestMethod]
        public void Logout()
        {
            var userAutenticationManager = new DalUserAutentication(_db);
            bool nullUser = userAutenticationManager.Logout(null);
            bool corretUser = userAutenticationManager.Logout(
                new User { Email = "admistator@test.com", Password = "testPassword" });
            bool notvalidEmailUser = userAutenticationManager.Logout(
                new User { Email = "admistatortest.com", Password = "testPassword" });
            bool notExistUser = userAutenticationManager.Logout(
                new User { Email = "TestCreate@test.com", Password = "testPassword" });
            bool nullPasswordUser = userAutenticationManager.Logout(
               new User { Email = "admistator@test.com", Password = "" });
            bool notValidPasswordUser = userAutenticationManager.Logout(
               new User { Email = "admistator@test.com", Password = "jjkfghksd" });

            Assert.IsTrue(corretUser);
            Assert.IsTrue(nullUser);
            Assert.IsTrue(notvalidEmailUser);
            Assert.IsTrue(notValidPasswordUser);
            Assert.IsTrue(nullPasswordUser);
            Assert.IsTrue(notExistUser);
        }
    }
}
