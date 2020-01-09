using Fin.ApplicationCore.Entities;
using Fin.ApplicationCore.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fin.Infrastructure.Data
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context): base(context)
        {

        }

        public User GetUser(int id)
        {

            return Get(id);
        }

        
    }
}
