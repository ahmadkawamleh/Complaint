using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ComplaintAttachment : BaseEntity
    {
        public string Path { get; set; }
        public string Title { get; set; }
        public string OriginalTitleName { get; set; }
        public int ComplaintId { get; set; }
        [ForeignKey("ComplaintId")]
        public Complaint Complaint { get; set; }
    }
}
