using Fin.ApplicationCore.Interfaces.Repositories;
using Fin.ApplicationCore.Interfaces.Services;
using Fin.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fin.ApplicationCore.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository) {
            this._loanRepository = loanRepository;
        }
        public async Task<List<LoanModel>> FindLoans(int skip, int take, string searchText)
        {
            return await this._loanRepository.FindLoans(skip, take, searchText);
            //var basedQuery = Enumerable.Empty<LoanModel>().AsQueryable();

            //var basedQuery = this._loanRepository.GetAll().AsQueryable();
            //var loans = basedQuery.Select(loan => new LoanModel()
            //{
            //    CustomerId = loan.CustomerId,
            //    CustomerName = loan.Customer != null ? loan.Customer.Name : string.Empty,
            //    InitialLoanAmount = loan.InitialLoanAmount,
            //    DateGranted = loan.DateGranted,
            //    PeriodMonths = loan.PeriodMonths,
            //    CapitalOutstanding = loan.CapitalOutstanding
            //});

            //return await Task.Run(() => loans.OrderBy(x => x.CustomerId).Skip(skip).Take(take).ToList());
        }
    }
}
