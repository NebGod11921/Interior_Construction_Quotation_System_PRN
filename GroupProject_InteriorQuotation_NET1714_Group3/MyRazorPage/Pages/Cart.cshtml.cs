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
        public ProductDto getProductById1(int productId)
        {
            var p = _p.GetProductById(productId);
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

        public void OnPostCaculator(int roomAre)
        {
            var carts = _cart.getAllCart();
            foreach (var item in carts)
            {
                var size = (int)_p.GetProductById(item.productId).Size;
                var categoryId = (int)_p.GetProductById(item.productId).Category.Id;
                item.quantity =  calProduct(roomAre,size, categoryId);
                _cart.UpdateCart(item.Id, item.quantity);
            }
            OnGet();
        }

        private int calProduct(int rAre, int productsize, int category)
        {
            double requiredAreaWithBuffer = rAre * 1.1;
            double numberOfProducts = 0;
            switch (category)
            {
                case 1: // gh?
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 6;
                    break;
                case 2:// g??ng
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 2;
                    break;
                case 3:// gi??ng
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 1;
                    break;
                case 4:// ?èn 
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 5;
                    break;
                default:
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 3;
                    break;
            }
            return (int)Math.Ceiling(numberOfProducts);

        }
    }
}
