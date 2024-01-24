using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RoomProduct
    {
        public Guid? ProductId { get; set; }
        public Guid? RoomId { get; set; } 
        public virtual Product? Product { get; set; }
        public virtual Room? Room { get; set; }
    }
}
