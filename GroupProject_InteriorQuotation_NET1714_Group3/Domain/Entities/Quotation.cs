using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Quotation : BaseEntity
    {
        
        public string? QuotationName { get; set; }
        public float? UnitPrice { get; set; }
        public float? TotalPrice { get; set; }
        public byte? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual int? RoomId { get; set;}
        public virtual Room? Room { get; set; }
        public virtual IEnumerable<Payment>? Payments { get; set; }
        public virtual IEnumerable<ProductQuotation>? ProductQuotations { get; set; }
        
        public virtual User? User { get; set; }

    }
}
