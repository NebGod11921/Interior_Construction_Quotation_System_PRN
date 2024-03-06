    using Application.Interfaces;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CartService : ICartService
    {
        List<CartDTO> carts = new List<CartDTO>();

        private static CartService instance = null;
        private static object instanceLock = new object();

        public CartService() { }

        private int cartID;
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
        public int GetIdNew()
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
        public CartDTO getCartByID(int id)
        {
            CartDTO? c = carts.SingleOrDefault(project => project.Id == id);
            return c;
        }
        public void AddToCart(CartDTO cart)
        {
            if (checkProductInCart(cart.productId) == true)
            {
                var exitcart = carts.Where(c => c.Id == cartID && c.Id != 0).FirstOrDefault();
                exitcart.quantity++;
                UpdateCart(cartID, exitcart.quantity);
            }
            else
            {
                
                    cart.Id = GetIdNew();
                    cart.quantity = 1;
                    carts.Add(cart);
            }
           
        }

        public bool checkProductInCart(int pId)
        {
            var cart = carts.Where(x => x.productId == pId).FirstOrDefault();
            if (cart != null)
            {
                cartID = cart.Id;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DeleteCartAll()
        {
            carts.Clear();
        }
        public void DeleteCart(int cartid)
        {
            var cart = carts.FirstOrDefault(x => x.Id == cartid);
            if (cart != null)
            {
                carts.Remove(cart);
            }
        }

        public bool UpdateCart(int id, int quantity)
        {
            var exitcart = carts.Where(c => c.Id == id).FirstOrDefault();
            if (exitcart != null)
            {
                if(quantity <= 0)
                {
                    return false;
                }
                else
                {
                    exitcart.quantity = quantity;
                    return true;    
                }
            }
            return false;
        }
    }
}
