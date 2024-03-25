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
        public int GetItemIdNew(int cartID);
        public CartDTO getCartByID(int id);
        public ItemDTO getItemByID(int id);
        public List<ItemDTO> getItemByCartId(int id);
        public void AddToCart(CartDTO cart);
        public void DeleteCartAll();
        public void DeleteItemInCart(int cartid, int itemid);
        public void UpdateCartItems(List<ItemDTO> updatedItems);
        public void UpdateCartItem(ItemDTO updatedItem);
    }
}
