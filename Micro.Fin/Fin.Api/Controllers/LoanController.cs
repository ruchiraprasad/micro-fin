using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fin.ApplicationCore.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepository _loanRepository;

        public LoanController(ILoanRepository loanRepository)
        {
            this._loanRepository = loanRepository;
        }

        [HttpGet("search-loans")]
        public async Task<ActionResult> SearchLoans(int skip, int take, string searchText)
        {
            var loans = await this._loanRepository.FindLoans(skip, take, searchText);
            return Ok(loans);
        }

        // GET: api/Loan
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Loan/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Loan
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Loan/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
