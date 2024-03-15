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
        public int GetCartIdNew();
        public int GetItemIdNew();
        public CartDTO getCartByID(int id);
        public ItemDTO getItemByID(int id);
        public List<ItemDTO> getItemByCartId(int id);
        public void AddToCart(CartDTO cart);
        public void DeleteCartAll();
        public void DeleteCart(int cartid);
        public void UpdateCartItems(List<ItemDTO> updatedItems);
    }
}
