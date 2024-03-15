using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public int cartId { get; set; }
        public int productId { get; set; }
        public int quanity { get; set; }
    }
}
