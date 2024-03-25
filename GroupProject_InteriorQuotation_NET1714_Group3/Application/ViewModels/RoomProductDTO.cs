using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class RoomProductDTO
    {
        public int? ProductId { get; set; }
        public int? RoomId { get; set; }
        public int? Quantity { get; set; }
        public float? ActualPrice { get; set; }
    }
}
