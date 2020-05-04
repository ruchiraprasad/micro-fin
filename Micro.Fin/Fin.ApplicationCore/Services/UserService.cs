using Fin.ApplicationCore.Entities;
using Fin.ApplicationCore.Interfaces.Repositories;
using Fin.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fin.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser(int id)
        {
            return _userRepository.Get(id);
        }

        public async Task<User> Authenticate(string userName, string password)
        {
            var user = await _userRepository.FindAsync(x=> x.UserName == userName && x.Password == password);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
