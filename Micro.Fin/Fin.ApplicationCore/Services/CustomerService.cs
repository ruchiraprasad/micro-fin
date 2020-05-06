using Fin.ApplicationCore.Entities;
using Fin.ApplicationCore.Interfaces.Repositories;
using Fin.ApplicationCore.Interfaces.Services;
using Fin.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fin.ApplicationCore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        public async Task<CustomerModel> SaveCustomer(CustomerModel customerModel)
        {
            if (customerModel.Id != null)
            {
                var existingCustomer = await this._customerRepository.GetAsync(customerModel.Id.Value);
                existingCustomer.Name = customerModel.Name;
                existingCustomer.Address = customerModel.Address;
                existingCustomer.Phone = customerModel.Phone;
                existingCustomer.Comment = customerModel.Comment;
                existingCustomer.UpdatedBy = "ruchira";
                existingCustomer.UpdatedOn = DateTime.Now;
                var result = await this._customerRepository.UpdateAsyn(existingCustomer, customerModel.Id);
            }
            else
            {
                var customer = new Customer()
                {
                    Name = customerModel.Name,
                    Address = customerModel.Address,
                    Phone = customerModel.Phone,
                    Comment = customerModel.Comment,
                    CreatedBy = "ruchira",
                    CreatedOn = DateTime.Now
                };

                var result = await this._customerRepository.AddAsyn(customer);
                customerModel.Id = result.Id;
            }
            
            return customerModel;
        }
    }
}
