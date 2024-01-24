using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Color : BaseEntity
    {
        
        public string ColourName { get; set; }
        public virtual IEnumerable<Product> Products { get; set;}
    }
}
