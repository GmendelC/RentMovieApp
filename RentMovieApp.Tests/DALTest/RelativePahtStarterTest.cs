using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace RentMovieApp.Tests.DALTest
{
    /// <summary>
    /// Summary description for RelativePahtStarterTest
    /// </summary>
    [TestClass]
    public class RelativePahtStarterTest
    {
        public RelativePahtStarterTest()
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
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
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
        public void ChangeDataDirectory()
        {
            DAL.RelativePahtStarter.ChangeDataDirectoryToSolutionFolder();

            var paht = AppDomain.CurrentDomain.GetData("DataDirectory");

            // for test upadate per device 
            var resPaht = @"C:\Users\CohenFamily\Desktop\RentMovieApp";

            Assert.AreEqual(paht, resPaht, paht.ToString());
            Debug.WriteLine(paht.ToString());
        }
    }
}
