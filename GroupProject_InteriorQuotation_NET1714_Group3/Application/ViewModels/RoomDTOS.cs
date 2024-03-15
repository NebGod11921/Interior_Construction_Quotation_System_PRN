using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
	public class RoomDTOS
	{
		public int Id { get; set; }
		public float Area { get; set; }
		public string? RoomDescription { get; set; }
		public DateTime? CreationDate {  get; set; }
        public virtual RoomType? RoomType { get; set; }
        public bool? IsDeleted { get; set; }
	}
}
