using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Image : BaseEntity
    {
        public string? ImageName { get; set; }
        public virtual IEnumerable<ProductImage>? ProductImages { get; set; }
    }
}
