using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductQuotation
    {
        public int? ProductId { get; set; }
        public int? QuotationId { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Quotation? Quotation { get; set; }
        //add new fields
        public int? Quantity { get; set; }
        public float? ActualPrice {  get; set; } 
    }
}
