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
    }
}
