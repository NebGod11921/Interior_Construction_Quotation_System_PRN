using Application.Interfaces;
using Application.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult OnPostAddToCartProduct(int pId) 
        {
            int cartId = _cart.GetIdNew();
            CartDTO cartForAdd = new CartDTO();
            cartForAdd.Id = cartId;
            cartForAdd.productId = pId;
            cartForAdd.quantity = 1;
            _cart.AddToCart(cartForAdd);
            return RedirectToPage("/Cart");
        }
    }
}
