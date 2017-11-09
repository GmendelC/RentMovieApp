using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Infra
{
    public interface IUserAutenticationService : IDisposable
    {
        // dispose because the db connection dispose
        bool CreateUser(User newUser);
        bool UpdateUser(User updateUser);

        bool Login(User user);
        bool Logout(User user);
    }
}
