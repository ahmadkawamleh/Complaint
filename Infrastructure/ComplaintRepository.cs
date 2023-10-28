using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ComplaintRepository : GenericRepository<Complaint, AppDbContext>, IComplaintRepository
    {
        private readonly List<string> includes = new List<string>() { "Attachments", "Demands" };
        public ComplaintRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public async Task<Complaint> GetComplaintById(int id, string userId, CancellationToken cancellationToken)
        {
            return await base.FindById(id, userId, includes, cancellationToken);
        }
        public async Task<List<Complaint>> GetAllComplaint(int pageNumber, int pageSize, string userId, CancellationToken cancellationToken)
        {
            return await base.GetAll(pageNumber, pageSize, userId, includes, cancellationToken);
        }
    }
}
