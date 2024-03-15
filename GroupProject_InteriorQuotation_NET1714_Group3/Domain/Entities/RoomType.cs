    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RoomType : BaseEntity
    {
        public string? RoomTypeName {  get; set; }
        public string? RoomTypeDescription { get; set; }
        public virtual IEnumerable<Room>? Rooms { get; set; }
    }
}
