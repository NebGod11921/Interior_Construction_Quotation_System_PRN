using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class RoomHomePageTitle
    {
        public string RoomType { get; set; }
        public List<RoomHomePageDTO> RoomInType { get; set; }
    }
}
