using Fin.ApplicationCore.Entities;
using Fin.ApplicationCore.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fin.Infrastructure.Data
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext context) : base(context)
        {

        }
    }
}
