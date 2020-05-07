using Fin.ApplicationCore.Entities;
using Fin.ApplicationCore.Interfaces.Repositories;
using Fin.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Fin.Infrastructure.Data
{
    public class LoanDetailRepository : GenericRepository<LoanDetail>, ILoanDetailRepository
    {
        public LoanDetailRepository(DataContext context) : base(context)
        {

        }

        public async Task<LoanDetailModel> FindLastLoanDetail(int loanId)
        {
            var basedQuery = this.GetAll();
            basedQuery = basedQuery.Where(x => x.LoanId == loanId).OrderByDescending(o => o.Installment);
            var loanDetail = await basedQuery.Select(loanDetailModel => new LoanDetailModel()
            {
                Id = loanDetailModel.Id,
                LoanId = loanDetailModel.LoanId,
                Month = loanDetailModel.Month,
                MonthlyInterest = loanDetailModel.MonthlyInterest,
                Paid = loanDetailModel.Paid,
                LatePaid = loanDetailModel.LatePaid,
                PaidDate = loanDetailModel.PaidDate,
                Balance = loanDetailModel.Balance,
                InterestType = loanDetailModel.InterestType,
                Installment = loanDetailModel.Installment,
                CapitalPaid = loanDetailModel.CapitalPaid,
            }).FirstOrDefaultAsync();

            return loanDetail;
        }
    }
}
