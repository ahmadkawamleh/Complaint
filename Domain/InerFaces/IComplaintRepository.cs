using Domain.Entities;

namespace Domain
{
    public interface IComplaintRepository : IGenericRepository<Complaint>
    {
        Task<List<Complaint>> GetAllComplaint(int pageNumber, int pageSize, string userId, bool isAdmin, CancellationToken cancellationToken);
        Task<Complaint> GetComplaintById(int id, string userId, CancellationToken cancellationToken);
    }
}