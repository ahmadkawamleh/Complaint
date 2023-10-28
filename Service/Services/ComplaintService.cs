using AutoMapper;
using Domain;
using Domain.Entities;
using Service.DTO;
using Service.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ComplaintService(IComplaintRepository complaintRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _complaintRepository = complaintRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Create(ComplaintDTO complaintDTO, CancellationToken cancellationToken)
        {
            var mapper = _mapper.Map<Complaint>(complaintDTO);
            _complaintRepository.Add(mapper);
            await _unitOfWork.SaveChanges(cancellationToken);
            return mapper.Id;
        }

        public async Task<ComplaintDTO> FindByIdAsync(int Id, string userId, CancellationToken cancellationToken)
        {
            var result = await _complaintRepository.GetComplaintById(Id, userId, cancellationToken);
            return _mapper.Map<ComplaintDTO>(result);
        }

        public async Task<List<ComplaintDTO>> GetAllAsync(int pageNumber, int pageSize, string userId, CancellationToken cancellationToken)
        {
            var result = await _complaintRepository.GetAllComplaint(pageNumber, pageSize, userId,cancellationToken);
            return _mapper.Map<List<ComplaintDTO>>(result);
        }

        public async Task Update(ComplaintDTO complaintDTO, CancellationToken cancellationToken)
        {
            var mapper = _mapper.Map<Complaint>(complaintDTO);
            _complaintRepository.Update(mapper);
            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
