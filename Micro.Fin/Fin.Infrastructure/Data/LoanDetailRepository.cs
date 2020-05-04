using Fin.ApplicationCore.Entities;
using Fin.ApplicationCore.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fin.Infrastructure.Data
{
    public class LoanDetailRepository : GenericRepository<LoanDetail>, ILoanDetailRepository
    {
        public LoanDetailRepository(DataContext context) : base(context)
        {

        }
    }
}
