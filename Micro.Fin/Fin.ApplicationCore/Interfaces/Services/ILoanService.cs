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
        Task<LoanCreateModel> CreateLoan(LoanCreateModel loanCreateModel, string username);
        Task<LoanDetailModel> UpdateLoanDetail(LoanDetailModel loanDetailModel, string username);
        Task<LoanDetailModel> CreateLoanDetail(LoanDetailModel loanDetailModel, string username);
        Task<List<LoanDetailModel>> GetLoanDetails(int loanId);
        Task<List<LoanDetailModel>> CalculateInterest(int loanId, int loanDetailId, InterestType interestType, string username);
    }
}
