using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductQuotation
    {
        public Guid ProductId { get; set; }
        public Guid QuotationId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Quotation Quotation { get; set; }
    }
}
