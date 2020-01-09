using Fin.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fin.ApplicationCore.Interfaces.Services
{
    public interface IUserService
    {
        User GetUser(int id);
    }
}
