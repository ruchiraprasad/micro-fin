using Fin.ApplicationCore.Entities;
using Fin.ApplicationCore.Entities.Enums;
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
            //return await Task.Run(() => loans.OrderBy(x => x.CustomerId).Skip(skip).Take(take).ToList());
        }

        public async Task<LoanCreateModel> CreateLoan(LoanCreateModel loanCreateModel)
        {
            var loan = new Loan()
            {
                CustomerId = loanCreateModel.CustomerId,
                InitialLoanAmount = loanCreateModel.InitialLoanAmount,
                DateGranted = loanCreateModel.DateGranted,
                PeriodMonths = loanCreateModel.PeriodMonths,
                Interest = loanCreateModel.Interest,
                Security = loanCreateModel.Security,
                PropertyValue = loanCreateModel.PropertyValue != null ? loanCreateModel.PropertyValue.Value : 0,
                CapitalOutstanding = loanCreateModel.CapitalOutstanding,

                CreatedBy = "ruchira",
                CreatedOn = DateTime.Now
            };

            // Add Loan Details..
            decimal totalInterest = 0;
            for (int i = 1; i <= loanCreateModel.PeriodMonths; i++)
            {
                totalInterest += loanCreateModel.CapitalOutstanding * loanCreateModel.Interest / 100;
                var loanDetail = new LoanDetail()
                {
                    Loan = loan,
                    Installment = i,
                    Month = loanCreateModel.DateGranted.AddMonths(i - 1),
                    MonthlyInterest = loanCreateModel.CapitalOutstanding * loanCreateModel.Interest / 100,
                    PaidDate = loanCreateModel.DateGranted.AddMonths(i - 1),
                    Balance = loanCreateModel.CapitalOutstanding + totalInterest,
                    InterestType = InterestType.SimpleInterest,
                    CreatedBy = "ruchira",
                    CreatedOn = DateTime.Now
                };

                loan.LoanDetails.Add(loanDetail);
            }

            var response = await this._loanRepository.AddAsyn(loan);
            loanCreateModel.Id = response.Id;
            return loanCreateModel;
        }
    }
}
