using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public float Area { get; set; }
        public string RoomDescription { get; set; }
        public int RoomTypeId { get; set; }
    }
}
