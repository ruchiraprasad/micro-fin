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
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(DataContext context) : base(context)
        {
            
        }

        public async Task<List<LoanModel>> FindLoans(int skip, int take, string searchText)
        {
            //var basedQuery = Enumerable.Empty<LoanModel>().AsQueryable();

            var basedQuery = this.GetAll();
            var loans = basedQuery.Select(loan => new LoanModel()
            {
                CustomerId = loan.CustomerId,
                CustomerName = loan.Customer != null ? loan.Customer.Name : string.Empty,
                InitialLoanAmount = loan.InitialLoanAmount,
                DateGranted = loan.DateGranted,
                PeriodMonths = loan.PeriodMonths,
                CapitalOutstanding = loan.CapitalOutstanding
            });

            if (!string.IsNullOrEmpty(searchText))
            {
                loans = loans.Where(x => x.CustomerName.Contains(searchText));
            }

            return await loans.OrderBy(x => x.CustomerId).Skip(skip).Take(take).ToListAsync();
            //return await Task.Run(() => loans.OrderBy(x => x.CustomerId).Skip(skip).Take(take).ToList());
        }
    }
}
