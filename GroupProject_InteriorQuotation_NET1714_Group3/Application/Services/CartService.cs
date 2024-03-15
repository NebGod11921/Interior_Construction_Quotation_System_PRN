using Application.Interfaces;
using Application.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CartService : ICartService
    {
        List<CartDTO> carts = new List<CartDTO>();
        List<ItemDTO> items = new List<ItemDTO>();
        private static CartService instance = null;
        private static object instanceLock = new object();

        public CartService() { }
        private int cartID;
        private int? cartIdToBuyCont = 0;
        public static CartService Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CartService();
                    }
                    return instance;
                }
            }
        }
        public List<CartDTO> getAllCart() => carts.OrderByDescending(c => c.Id).ToList();
        public List<ItemDTO> getItemByCartId(int cartID) => carts.SelectMany(cart => cart.Items)
                .Where(item => item.cartId == cartID)
                .ToList();
        public int GetCartIdNew()
        {
            if (carts.Any())
            {
                var id = carts.Max(x => x.Id);
                return id + 1;
            }
            else
            {
                return 1;
            }
        }
        public int GetItemIdNew()
        {
            if (items.Any())
            {
                var id = items.Max(x => x.Id);
                return id + 1;
            }
            else
            {
                return 1;
            }
        }
        public CartDTO getCartByID(int id)
        {
            CartDTO? c = carts.SingleOrDefault(project => project.Id == id);
            return c;
        }
        public ItemDTO getItemByID(int id)
        {
            ItemDTO? c = items.SingleOrDefault(project => project.Id == id);
            return c;
        }

        public void AddToCart(CartDTO cart)
        {
            if(cartIdToBuyCont == 0 || cartIdToBuyCont == null)
            {
                carts.Add(cart);
            }
            else
            {
                carts.Add(cart);
            }
            //if (checkProductInCart(cart.productId) == true)
            //{
            //    var exitcart = carts.Where(c => c.Id == cartID && c.Id != 0).FirstOrDefault();
            //    exitcart.quantity++;
            //    UpdateCart(cartID, exitcart.quantity);
            //}
            //else
            //{
                
            //        cart.Id = GetIdNew();
            //        cart.quantity = 1;
            //        carts.Add(cart);
            //}
           
        }

        public void DeleteCartAll()
        {
            throw new NotImplementedException();
        }

        public void DeleteCart(int cartid)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCart(int id, int quantity)
        {
            throw new NotImplementedException();
        }

        //public bool checkItemInCart(int itemId)
        //{
        //    var cart = carts.Where(x => x.productId == pId).FirstOrDefault();
        //    if (cart != null)
        //    {
        //        cartID = cart.Id;
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool checkItemInCart(int itemId)
        //{
        //    var cart = carts.Where(x => x.productId == pId).FirstOrDefault();
        //    if (cart != null)
        //    {
        //        cartID = cart.Id;
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //public void DeleteCartAll()
        //{
        //    carts.Clear();
        //}
        //public void DeleteCart(int cartid)
        //{
        //    var cart = carts.FirstOrDefault(x => x.Id == cartid);
        //    if (cart != null)
        //    {
        //        carts.Remove(cart);
        //    }
        //}

        public void UpdateCartItems(List<ItemDTO> updatedItems)
        {
            foreach (var updatedItem in updatedItems)
            {
                var existingItem = items.FirstOrDefault(i => i.Id == updatedItem.Id);

                if (existingItem != null)
                {
                    existingItem.productId = updatedItem.productId;
                    existingItem.cartId = updatedItem.cartId;
                    existingItem.quanity = updatedItem.quanity;

                }
            }
        }

        }
}
