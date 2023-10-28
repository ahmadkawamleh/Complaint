using Domain.Entities;

namespace Service.DTO
{
    public class ComplaintDTO
    {
        public string ComplaintText { get; set; }
        public ICollection<ComplaintAttachmentDTO> Attachments { get; set; }
        public ICollection<DemandsDTO> Demands { get; set; }
        public int ApprovalStatus { get; set; }
        public string UserId { get; set; }
    }
}