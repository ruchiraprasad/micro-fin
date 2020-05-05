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
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepository _loanRepository;
        private readonly ILoanDetailRepository _loanDetailRepository;
        private readonly ILoanService _loanLoanService;

        public LoanController(ILoanRepository loanRepository, ILoanService loanService, ILoanDetailRepository loanDetailRepository)
        {
            this._loanRepository = loanRepository;
            this._loanLoanService = loanService;
            this._loanDetailRepository = loanDetailRepository;
        }

        [HttpGet("search-loans")]
        public async Task<ActionResult> SearchLoans(int skip, int take, string searchText)
        {
            var loans = await this._loanRepository.FindLoans(skip, take, searchText);
            return Ok(loans);
        }

        // POST: api/loan
        [HttpPost("create")]
        public async Task<ActionResult> Create(LoanCreateModel loanCreateModel)
        {
            if(loanCreateModel != null)
            {
                var result = await this._loanLoanService.CreateLoan(loanCreateModel);
                return Ok(result);
            }

            return null;
        }

        [HttpGet("loan-details/{loanId}")]
        public async Task<ActionResult> GetLoanDetails(int loanId)
        {
            var loanDetails = await this._loanDetailRepository.FindAllAsync(x => x.LoanId == loanId);
            return Ok(loanDetails);
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
