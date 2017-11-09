using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL.MovieRentContext
{
    public class MovieRentalInitializer : CreateDatabaseIfNotExists<RentalMovieContext>
    {
        protected override void Seed(RentalMovieContext context)
        {
            base.Seed(context);

            context.Database.ExecuteSqlCommand("CREATE UNIQUE INDEX IX_User_Email ON User (Email)");

        }
    }
}
