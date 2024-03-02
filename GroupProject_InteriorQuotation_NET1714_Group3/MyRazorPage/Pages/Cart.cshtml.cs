using Application.Interfaces;
using Application.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            return _p.GetAllProductById(productId);
        }
        public void OnGet() 
        {
            carts = _cart.getAllCart();
        }
    }
}
