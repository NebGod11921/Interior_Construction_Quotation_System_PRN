using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Room : BaseEntity
    {
        public float Area { get; set; }
        public string? RoomDescription { get; set; }
        public virtual IEnumerable<RoomProduct>? RoomProducts { get; set; }
        
        public virtual RoomType? RoomType { get; set; }
        public virtual Quotation? Quotation { get; set; }
    }
}
