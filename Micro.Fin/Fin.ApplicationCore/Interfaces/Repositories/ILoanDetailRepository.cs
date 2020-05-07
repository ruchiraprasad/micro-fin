using Fin.ApplicationCore.Entities;
using Fin.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fin.ApplicationCore.Interfaces.Repositories
{
    public interface ILoanDetailRepository : IGenericRepository<LoanDetail>
    {
        Task<LoanDetailModel> FindLastLoanDetail(int loanId);
    }
}
