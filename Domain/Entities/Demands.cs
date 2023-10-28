using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Demands : BaseEntity
    {
        public string ArabicDemand { get; set; }
        public string EnglishDemand { get; set; }
        public int ComplaintId { get; set; }
        [ForeignKey("ComplaintId")]
        public Complaint Complaint { get; set; }
    }
}
