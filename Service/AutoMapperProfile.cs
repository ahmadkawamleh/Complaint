using AutoMapper;
using Domain.Entities;
using Service.DTO;

namespace Service
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ComplaintDTO, Complaint>().ReverseMap();
            CreateMap<ComplaintAttachmentDTO, ComplaintAttachment>().ReverseMap();
            CreateMap<DemandsDTO, Demands>().ReverseMap();
        }
    }
}
