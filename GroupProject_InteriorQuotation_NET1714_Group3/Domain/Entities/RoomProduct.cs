using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RoomProduct 
    {
        public int? ProductId { get; set; }
        public  int? RoomId { get; set; } 
        public virtual Product? Product { get; set; }
        public virtual Room? Room { get; set; }
        public int? Quantity { get; set; }
        public float? ActualPrice { get; set; }
    }
}
