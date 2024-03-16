using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class CartDTO
    {
        //public int Id { get; set; }
        //public int productId { get; set; }
        //public int quantity { get; set; }

        public int Id { get; set; }
        public int? roomId { get; set; }
        public int? rType { get; set; }
        public float? rAre { get; set; }
        public string rDescrip { get; set; }
        public virtual List<ItemDTO> Items { get; set; }
    }
}
