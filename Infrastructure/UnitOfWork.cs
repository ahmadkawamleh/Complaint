using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            var saveResult = await _appDbContext.SaveChangesAsync(cancellationToken);
            return saveResult;
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
