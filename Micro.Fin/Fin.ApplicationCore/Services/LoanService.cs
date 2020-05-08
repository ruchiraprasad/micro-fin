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
                    Month = loanCreateModel.DateGranted.AddMonths(i),
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

        public async Task<List<LoanDetailModel>> GetLoanDetails(int loanId)
        {
            var loanDetails = await this._loanDetailRepository.FindAllAsync(x => x.LoanId == loanId);
            var result = loanDetails.Select(x => new LoanDetailModel()
            {
                Id = x.Id,
                LoanId = x.LoanId,
                Month = x.Month,
                MonthlyInterest = x.MonthlyInterest,
                Paid = x.Paid,
                LatePaid = x.LatePaid,
                PaidDate = x.PaidDate,
                Balance = x.Balance,
                InterestType = x.InterestType,
                Installment = x.Installment,
                CapitalPaid = x.CapitalPaid,
                IsCompoundInterest = x.InterestType == InterestType.CompoundInterest ? true : false
            }).ToList();

            if(result.FirstOrDefault(x => x.PaidDate != null) != null)
            {
                var currentRecord = result.Where(x => x.PaidDate != null).OrderByDescending(o => o.Installment).FirstOrDefault();
                currentRecord.Editing = true;
                if (result.Where(x => x.Installment == currentRecord.Installment + 1).FirstOrDefault() != null)
                {
                    result.Where(x => x.Installment == currentRecord.Installment + 1).FirstOrDefault().Editing = true;
                }
            }
            else
            {
                result.OrderBy(o => o.Installment).FirstOrDefault().Editing = true;
            }

            return result;
        }

        public async Task<LoanDetailModel> UpdateLoanDetail(LoanDetailModel loanDetailModel)
        {
            var _existingLoan = this._loanRepository.GetAllIncluding(e => e.LoanDetails).Where(x => x.Id == loanDetailModel.LoanId).FirstOrDefault();
            var _existingLoanDetail = _existingLoan.LoanDetails.FirstOrDefault(x => x.Id == loanDetailModel.Id);
            if(loanDetailModel.InterestType == InterestType.SimpleInterest)
            {
                if (_existingLoanDetail.Paid == null)
                {
                    _existingLoanDetail.Balance -= loanDetailModel.Paid.Value;
                }
                else
                {
                    _existingLoanDetail.Balance += _existingLoanDetail.Paid.Value;
                    _existingLoanDetail.Balance -= loanDetailModel.Paid.Value;
                }
            }
            
            _existingLoanDetail.Paid = loanDetailModel.Paid;
            _existingLoanDetail.LatePaid = loanDetailModel.LatePaid;
            _existingLoanDetail.PaidDate = loanDetailModel.PaidDate;
            _existingLoanDetail.UpdatedBy = "ruchira";
            _existingLoanDetail.UpdatedOn = DateTime.Now;

            if (loanDetailModel.CapitalPaid != null && loanDetailModel.CapitalPaid > 0)
            {
                if(loanDetailModel.InterestType == InterestType.SimpleInterest)
                {
                    _existingLoan.UpdatedBy = "ruchira";
                    _existingLoan.UpdatedOn = DateTime.Now;
                    if (_existingLoanDetail.CapitalPaid == null)
                    {
                        _existingLoanDetail.Balance -= loanDetailModel.CapitalPaid.Value;
                        _existingLoan.CapitalOutstanding -= loanDetailModel.CapitalPaid.Value;
                    }
                    else
                    {
                        _existingLoanDetail.Balance += _existingLoanDetail.CapitalPaid.Value;
                        _existingLoanDetail.Balance -= loanDetailModel.CapitalPaid.Value;

                        _existingLoan.CapitalOutstanding += _existingLoanDetail.CapitalPaid.Value;
                        _existingLoan.CapitalOutstanding -= loanDetailModel.CapitalPaid.Value;
                    }
                    _existingLoanDetail.CapitalPaid = loanDetailModel.CapitalPaid;
                }
            }

            var _futureLoanDetails = _existingLoan.LoanDetails.Where(x => x.Installment > _existingLoanDetail.Installment).OrderBy(o => o.Installment);
            var _previousBalance = _existingLoanDetail.Balance;
            var _previousLoanDetail = _existingLoanDetail;
            foreach (var _futureLoanDetail in _futureLoanDetails)
            {
                if(_existingLoanDetail.InterestType == InterestType.SimpleInterest && _existingLoanDetail.CapitalPaid != null)
                {
                    _futureLoanDetail.MonthlyInterest = _existingLoan.CapitalOutstanding * _existingLoan.Interest / 100;
                    _futureLoanDetail.Balance = _previousBalance + _futureLoanDetail.MonthlyInterest;
                    _futureLoanDetail.InterestType = InterestType.SimpleInterest;
                }
                else if (_futureLoanDetail.InterestType == InterestType.SimpleInterest && _previousLoanDetail.InterestType == InterestType.SimpleInterest)
                {
                    _futureLoanDetail.MonthlyInterest = _existingLoan.CapitalOutstanding * _existingLoan.Interest / 100;
                    _futureLoanDetail.Balance = _previousBalance + _futureLoanDetail.MonthlyInterest;
                    _futureLoanDetail.InterestType = InterestType.SimpleInterest;
                }
                _previousBalance = _futureLoanDetail.Balance;
                _previousLoanDetail = _futureLoanDetail;
            }

            var result = await this._loanRepository.UpdateAsyn(_existingLoan, loanDetailModel.LoanId);
            loanDetailModel.Balance = _existingLoanDetail.Balance;
            return loanDetailModel;
        }

        public async Task<LoanDetailModel> CreateLoanDetail(LoanDetailModel loanDetailModel)
        {
            var loan = await this._loanRepository.GetAsync(loanDetailModel.LoanId);
            var lastLoanDetail = await this._loanDetailRepository.FindLastLoanDetail(loanDetailModel.LoanId);
            var loanDetail = new LoanDetail()
            {
                LoanId = loanDetailModel.LoanId,
                Installment = lastLoanDetail.Installment + 1,
                Month = lastLoanDetail.Month.AddMonths(1),
                MonthlyInterest = lastLoanDetail.InterestType == InterestType.CompoundInterest ? lastLoanDetail.Balance * loan.Interest / 100 :
                    loan.CapitalOutstanding * loan.Interest / 100,
                Balance = lastLoanDetail.InterestType == InterestType.CompoundInterest ?
                    lastLoanDetail.Balance + lastLoanDetail.Balance * loan.Interest / 100 :
                    loan.CapitalOutstanding + loan.CapitalOutstanding * loan.Interest / 100,
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

        public async Task<List<LoanDetailModel>> CalculateInterest(int loanId, int loanDetailId, InterestType interestType)
        {
            var _existingLoan = this._loanRepository.GetAllIncluding(e => e.LoanDetails).Where(x => x.Id == loanId).FirstOrDefault();
            var _existingLoanDetail = _existingLoan.LoanDetails.FirstOrDefault(x => x.Id == loanDetailId);
            _existingLoanDetail.InterestType = interestType;
            _existingLoanDetail.UpdatedBy = "ruchira";
            _existingLoanDetail.UpdatedOn = DateTime.Now;

            var _futureLoanDetails = _existingLoan.LoanDetails.Where(x => x.Installment > _existingLoanDetail.Installment).OrderBy(o => o.Installment);
            var _balanceTakeForward = _existingLoan.CapitalOutstanding;

            var _previousLoanDetail = _existingLoan.LoanDetails.FirstOrDefault(x => x.Installment == _existingLoanDetail.Installment - 1);
            if (_previousLoanDetail != null)
            {
                if (_previousLoanDetail.InterestType == InterestType.CompoundInterest)
                    _balanceTakeForward = _previousLoanDetail.Balance;
                else
                    _balanceTakeForward = _existingLoan.CapitalOutstanding;
            }

            if (interestType == InterestType.CompoundInterest)
            {
                _existingLoanDetail.MonthlyInterest = _balanceTakeForward * _existingLoan.Interest / 100;
            }
            else if(interestType == InterestType.SimpleInterest && _previousLoanDetail.InterestType == InterestType.CompoundInterest)
            {
                _existingLoanDetail.MonthlyInterest = _balanceTakeForward * _existingLoan.Interest / 100;
            }
            else
            {
                _existingLoanDetail.MonthlyInterest = _existingLoan.CapitalOutstanding * _existingLoan.Interest / 100;
            }

            _existingLoanDetail.Balance = _existingLoan.CapitalOutstanding + _existingLoanDetail.MonthlyInterest;

            var _previousBalance = _existingLoanDetail.Balance;
            foreach (var _futureLoanDetail in _futureLoanDetails)
            {
                if(interestType == InterestType.CompoundInterest)
                {
                    _futureLoanDetail.MonthlyInterest = _previousBalance * _existingLoan.Interest / 100;
                }
                else
                {
                    _futureLoanDetail.MonthlyInterest = _existingLoan.CapitalOutstanding * _existingLoan.Interest / 100;
                }
                
                _futureLoanDetail.Balance = _previousBalance + _futureLoanDetail.MonthlyInterest;
                _futureLoanDetail.InterestType = interestType;
                _futureLoanDetail.UpdatedBy = "ruchira";
                _futureLoanDetail.UpdatedOn = DateTime.Now;

                _previousBalance = _futureLoanDetail.Balance;
            }

            var result = await this._loanRepository.UpdateAsyn(_existingLoan, loanId);
            return await this.GetLoanDetails(loanId);
        }
    }
}
