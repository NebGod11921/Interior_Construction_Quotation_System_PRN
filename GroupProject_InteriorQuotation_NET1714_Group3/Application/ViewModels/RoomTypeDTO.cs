using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class RoomTypeDTO
    {
        public int Id { get; set; }
        public string RoomTypeName { get; set; }
        public string RoomTypeDescription { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public List<RoomHomePageDTO> Rooms { get; set; }
        public RoomTypeDTO()
        {
            Rooms = new List<RoomHomePageDTO>(); // Initialize the Rooms property
        }
    }
}
