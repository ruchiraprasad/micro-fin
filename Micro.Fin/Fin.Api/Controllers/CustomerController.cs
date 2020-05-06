using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fin.ApplicationCore.Interfaces.Repositories;
using Fin.ApplicationCore.Interfaces.Services;
using Fin.ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerRepository customerRepository, ICustomerService customerService)
        {
            this._customerRepository = customerRepository;
            this._customerService = customerService;
        }

        [HttpGet()]
        public async Task<ActionResult> GetCustomers()
        {
            var loans = await this._customerRepository.GetAllAsyn();
            return Ok(loans);
        }

        [HttpPost()]
        public async Task<IActionResult> Post(CustomerModel customerModel)
        {
            var result = await this._customerService.SaveCustomer(customerModel);
            return Ok(result);
        }
    }
}