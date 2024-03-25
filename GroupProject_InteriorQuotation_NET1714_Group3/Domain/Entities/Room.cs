using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("RoomTypeId")]
        public int RoomTypeId {  get; set; }
        public virtual RoomType? RoomType { get; set; }
        public virtual Quotation? Quotation { get; set; }
    }
}
