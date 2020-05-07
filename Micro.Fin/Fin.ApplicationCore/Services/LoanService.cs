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
        private readonly ILoanDetailRepository _loanDetailRepository;

        public LoanService(ILoanRepository loanRepository, ILoanDetailRepository loanDetailRepository) {
            this._loanRepository = loanRepository;
            this._loanDetailRepository = loanDetailRepository;
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

        public async Task<LoanDetailModel> UpdateLoanDetail(LoanDetailModel loanDetailModel)
        {
            var existingLoanDetail = await this._loanDetailRepository.GetAsync(loanDetailModel.Id.Value);
            if (existingLoanDetail.Paid == null)
            {
                existingLoanDetail.Balance -= loanDetailModel.Paid.Value;
            }
            else
            {
                existingLoanDetail.Balance += existingLoanDetail.Paid.Value;
                existingLoanDetail.Balance -= loanDetailModel.Paid.Value;
            }
            existingLoanDetail.Paid = loanDetailModel.Paid;
            existingLoanDetail.LatePaid = loanDetailModel.LatePaid;
            existingLoanDetail.PaidDate = loanDetailModel.PaidDate;
            
            
            existingLoanDetail.UpdatedBy = "ruchira";
            existingLoanDetail.UpdatedOn = DateTime.Now;

            if (loanDetailModel.CapitalPaid != null && loanDetailModel.CapitalPaid > 0)
            {
                var existingLoan = await this._loanRepository.GetAsync(loanDetailModel.LoanId);
                existingLoan.UpdatedBy = "ruchira";
                existingLoan.UpdatedOn = DateTime.Now;

                if(existingLoanDetail.CapitalPaid == null)
                {
                    existingLoanDetail.Balance -= loanDetailModel.CapitalPaid.Value;
                    existingLoan.CapitalOutstanding -= loanDetailModel.CapitalPaid.Value;
                }
                else
                {
                    existingLoanDetail.Balance += existingLoanDetail.CapitalPaid.Value;
                    existingLoanDetail.Balance -= loanDetailModel.CapitalPaid.Value;

                    existingLoan.CapitalOutstanding += existingLoanDetail.CapitalPaid.Value;
                    existingLoan.CapitalOutstanding -= loanDetailModel.CapitalPaid.Value;
                }

                existingLoanDetail.Loan = existingLoan;
            }

            existingLoanDetail.CapitalPaid = loanDetailModel.CapitalPaid;

            var result = await this._loanDetailRepository.UpdateAsyn(existingLoanDetail, loanDetailModel.Id);

            loanDetailModel.Balance = existingLoanDetail.Balance;
            return loanDetailModel;
        }

        public async Task<LoanDetailModel> CreateLoanDetail(LoanDetailModel loanDetailModel)
        {
            var loan = await this._loanRepository.GetAsync(loanDetailModel.LoanId);
            var latsLoanDetail = await this._loanDetailRepository.FindLastLoanDetail(loanDetailModel.LoanId);
            var loanDetail = new LoanDetail()
            {
                LoanId = loanDetailModel.LoanId,
                Installment = latsLoanDetail.Installment + 1,
                Month = latsLoanDetail.Month.AddMonths(1),
                MonthlyInterest = loan.CapitalOutstanding * loan.Interest / 100,
                Balance = latsLoanDetail.Balance + loan.CapitalOutstanding * loan.Interest / 100,
                InterestType = InterestType.SimpleInterest,
                CreatedBy = "ruchira",
                CreatedOn = DateTime.Now
            };

            var result = await this._loanDetailRepository.AddAsyn(loanDetail);
            loanDetailModel.Id = result.Id;
            loanDetailModel.Installment = result.Installment;
            loanDetailModel.Month = result.Month;
            loanDetailModel.MonthlyInterest = result.MonthlyInterest;
            loanDetailModel.Balance = result.Balance;
            loanDetailModel.InterestType = result.InterestType;

            return loanDetailModel;
        }
    }
}
