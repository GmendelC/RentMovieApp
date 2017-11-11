using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using System.IO;

namespace RentMovieApp.Tests.DALTest
{
    /// <summary>
    /// Summary description for FileStringLoggerTest
    /// </summary>
    [TestClass]
    public class FileStringLoggerTest
    {
        public FileStringLoggerTest()
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
        public void CtorTest()
        {
            string path = @"C:\Users\CohenFamily\Desktop\Gabriel\Logger.txt";

            FileStringLogger pathLogger = new FileStringLogger(path);

            Assert.AreEqual(path, pathLogger.LoggerPath, "Logger Change Path.");

            FileStringLogger defaltLogger = new FileStringLogger();

            string dataPath = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            string defaultPath = Path.Combine(dataPath, "Log.txt");

            Assert.AreEqual(defaultPath, defaltLogger.LoggerPath);
        }

        [TestMethod]
        public void LoggTest()
        {
            FileStringLogger defaltLogger = new FileStringLogger(@"C:\USERS\COHENFAMILY\DESKTOP\RENTMOVIEAPP\DAL\Log.txt");
            string[] lines = { $"Test line 1 {DateTime.Now}", "Test line 2 {DateTime.Now}" };

            defaltLogger.Log(lines[0]);

            bool exist = File.Exists(defaltLogger.LoggerPath);

            Assert.IsTrue(exist, "Logger Can't create file");

            defaltLogger.Log(lines[1]);

            string[] Loggerlines = File.ReadAllLines(defaltLogger.LoggerPath);
            int loggerLeng = Loggerlines.Length;

            foreach (var line in lines)
            {
                bool existsLine = Array
                    .Exists<string>(Loggerlines, str => str == line);
                Assert.IsTrue(existsLine, "Logger Cnat write in file");
            }

            defaltLogger.Log(null);

            int finalLoggerLeng = File.ReadAllLines(defaltLogger.LoggerPath).Length;

            Assert.AreEqual(finalLoggerLeng, loggerLeng, "Logg Null string error");
        }
    }
}
