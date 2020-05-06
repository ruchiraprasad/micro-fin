using Fin.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fin.ApplicationCore.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<CustomerModel> SaveCustomer(CustomerModel customerModel);
    }
}
