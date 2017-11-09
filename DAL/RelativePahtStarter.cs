using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DAL
{
    public static class RelativePahtStarter
    {
        // this class is change upadate reltive Path to conncetion string or like this
        public static void ChangeDataDirectoryToSolutionFolder()
        {
            // Change the variable of dataDirectory to SolutionFolder Path

            string currentDirectory = Environment.CurrentDirectory;

            // to do in while to search diretory of name RentMovieApp
            string binDir = Path.GetDirectoryName(currentDirectory);
            string appDir = Path.GetDirectoryName(binDir);
            string solutionDirectory = Path.GetDirectoryName(appDir);

            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            AppDomain.CurrentDomain.SetData("DataDirectory", solutionDirectory);
           // string path = (System.IO.Path.GetDirectoryName(executable));
        }
    }
}
