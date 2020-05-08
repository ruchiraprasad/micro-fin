using Fin.ApplicationCore.Entities.Enums;
using Fin.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fin.ApplicationCore.Interfaces.Services
{
    public interface ILoanService
    {
        Task<List<LoanModel>> FindLoans(int skip, int take, string searchText);
        Task<LoanCreateModel> CreateLoan(LoanCreateModel loanCreateModel);
        Task<LoanDetailModel> UpdateLoanDetail(LoanDetailModel loanDetailModel);
        Task<LoanDetailModel> CreateLoanDetail(LoanDetailModel loanDetailModel);
        Task<List<LoanDetailModel>> GetLoanDetails(int loanId);
        Task<List<LoanDetailModel>> CalculateInterest(int loanId, int loanDetailId, InterestType interestType);
    }
}
