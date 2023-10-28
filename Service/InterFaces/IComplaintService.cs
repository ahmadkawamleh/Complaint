using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTO;

namespace Service.InterFaces
{
    public interface IComplaintService
    {
        Task<List<ComplaintDTO>> GetAllAsync(int pageNumber, int pageSize, string userId, bool isAdmin ,CancellationToken cancellationToken);
        Task<ComplaintDTO> FindByIdAsync(int Id, string userId, CancellationToken cancellationToken);
        Task<int> Create(ComplaintDTO complaintDTO, CancellationToken cancellationToken);
        Task Update(ComplaintDTO complaintDTO, CancellationToken cancellationToken);
    }
}
