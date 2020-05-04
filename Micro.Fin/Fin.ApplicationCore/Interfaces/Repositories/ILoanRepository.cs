using Fin.ApplicationCore.Entities;
using Fin.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fin.ApplicationCore.Interfaces.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<List<LoanModel>> FindLoans(int skip, int take, string searchText);
    }
}
