using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public  class RoomHomePageDTO
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public List<ImageRoomInTypeDTO> ImageProduct { get; set; }
        public float AreaRoom { get; set; }

    }
}
