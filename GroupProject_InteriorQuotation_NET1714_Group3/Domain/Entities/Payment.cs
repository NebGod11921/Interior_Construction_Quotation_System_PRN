using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        public float? Amount { get; set; }
        public string? PaymentName {  get; set; }
        public DateTime? CreatedDate { get; set; }
        
        public virtual Quotation Quotation { get; set; }
    }
}
