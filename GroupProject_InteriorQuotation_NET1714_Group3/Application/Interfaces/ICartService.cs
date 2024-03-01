using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICartService
    {
        public List<CartDTO> getAllCart();
        public int GetIdNew();
        public CartDTO getCartByID(int id);
        public void AddToCart(CartDTO cart);
        public void DeleteCartAll();
        public void DeleteCart(int cartid);
        public bool UpdateCart(DateTime startdate, DateTime endDate, int id);
    }
}
