using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public float Size { get; set; }
        public float Price {  get; set; }
        
        
        public virtual Color? Color { get; set; }
        
        public virtual Material? Material { get; set; }
        
        public virtual Category? Category { get; set; }
        public virtual IEnumerable<ProductImage>? ProductImages { get; set; }
        public virtual IEnumerable<RoomProduct>? RoomProducts { get; set; }
        public virtual IEnumerable<ProductQuotation>? ProductQuotations { get; set; }

        public virtual Quotation? Quotation { get; set; }
        

    }
}
