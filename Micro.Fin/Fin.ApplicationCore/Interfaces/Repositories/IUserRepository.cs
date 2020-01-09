using Fin.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fin.ApplicationCore.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetUser(int id);
       // User GetUserAsync(string username, string password);
    }
}
