using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Material : BaseEntity
    {
        public string? MaterialName { get; set; }
        public virtual IEnumerable<Product>? Products { get; set;}
    }
}
