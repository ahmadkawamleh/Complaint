using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Complaint : BaseEntity
    {
        public string ComplaintText { get; set; }
        public ICollection<ComplaintAttachment> Attachments { get; set; }
        public ICollection<Demands> Demands { get; set; }
        public int ApprovalStatus { get; set; }
    }
}
