using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class RoomTypeDTOS
    {
        public int Id { get; set; }
        public string? RoomTypeName { get; set; }
        public string? RoomTypeDescription { get; set; }
        public virtual IEnumerable<RoomDTOS>? Rooms { get; set; }
    }
}
