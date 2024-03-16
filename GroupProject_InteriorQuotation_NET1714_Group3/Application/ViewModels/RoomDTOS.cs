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
	public class RoomDTOS
	{
       
		public float Area { get; set; }
		public string? RoomDescription { get; set; }
		public DateTime? CreationDate {  get; set; }
        public virtual RoomTypeDTOS? RoomType { get; set; }
        public bool? IsDeleted { get; set; }
	}
}
