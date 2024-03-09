using Application.Interfaces;
using Application.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace MyRazorPage.Pages
{
    public class CartModel : PageModel
    {
        private readonly ICartService _cart;
        private readonly IProductService _p;
        public CartModel(ICartService cart, IProductService p)
        {
            _cart = cart;
            _p = p;
        }
        public List<CartDTO> carts;
        public ProductDto getProductById(int productId)
        {
            var p = _p.GetProductByIdToCart(productId);
            return p;
        }
        public async Task OnGet() 
        {
            carts = _cart.getAllCart() ?? new List<CartDTO>();
        }

        public void OnPostAddToCartProduct(int pId) 
        {
            CartDTO cartForAdd = new CartDTO();
            cartForAdd.productId = pId;
            _cart.AddToCart(cartForAdd);
            OnGet();
        }

        public IActionResult OnPostAddToCartListProduct(int rId)
        {
            var room = _p.GetAllProductByRoomId(rId);
            foreach (var item in room)
            {
                CartDTO cartForAdd = new CartDTO();
                cartForAdd.productId = item.ProductId;
                _cart.AddToCart(cartForAdd);
                OnGet();
            }
            
            return RedirectToPage("/Cart");
        }

        public void OnPostUpdateQuantity(int id, int quantity)
        {
            var cart = _cart.getCartByID(id);
            if (cart != null)
            {
                cart.quantity = quantity;
                _cart.UpdateCart(id, cart.quantity);
            }
            OnGet();
        }

        public void OnPostDeleteCart(int cID)
        {
            var cart = _cart.getCartByID(cID);
            if (cart != null)
            {
                _cart.DeleteCart(cID);
            }
            OnGet();
        }
    }
}
