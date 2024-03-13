using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ProductDto
    {
      public string RoomName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public float Size { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public virtual Category Category { get; set; }
        public string Color { get; set; }

        public string Material { get; set; }

    }
}
