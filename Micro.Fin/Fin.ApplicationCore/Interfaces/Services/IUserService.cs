using Fin.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fin.ApplicationCore.Interfaces.Services
{
    public interface IUserService
    {
        User GetUser(int id);
        Task<User> Authenticate(string userName, string password);
    }
}
