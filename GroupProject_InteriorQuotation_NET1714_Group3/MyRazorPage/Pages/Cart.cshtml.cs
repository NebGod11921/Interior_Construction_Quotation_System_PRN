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
        private readonly IQuotationService _q;
        private readonly IRoomService _r;
        private readonly IRoomTypeService _rt;
        private float Are;
        public CartModel(ICartService cart, IProductService p, IQuotationService q, IRoomService r, IRoomTypeService rt)
        {
            _cart = cart;
            _p = p;
            _q = q;
            _r = r;
            _rt = rt;
        }
        public List<CartDTO> carts;
        public List<RoomTypeDTO> roomtypes; 
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
            roomtypes = await _rt.GetAllRoomTypeDTOs();
        }

        //public void OnPostAddToCartProduct(int pId) 
        //{
        //    CartDTO cartForAdd = new CartDTO();
        //    cartForAdd.Items.Add(new ItemDTO { productId = pId });
        //    _cart.AddToCart(cartForAdd);
        //    OnGet();
        //}

        public IActionResult OnPostAddToCartListProduct(int rId)
        {
            var cexits = _cart.getAllCart().Where(x => x.roomId == rId).FirstOrDefault();
            if (cexits == null)
            {
                var room = _p.GetAllProductByRoomId(rId);
                CartDTO cartForAdd = new CartDTO();
                cartForAdd.Id = _cart.GetCartIdNew();
                cartForAdd.roomId = rId;
                if (cartForAdd.Items == null)
                {
                    cartForAdd.Items = new List<ItemDTO>();
                }
                foreach (var item in room)
                {
                    cartForAdd.Items.Add(new ItemDTO
                    {
                        Id = _cart.GetItemIdNew(),
                        productId = item.ProductId,
                        cartId = cartForAdd.Id,
                        quanity = 1
                    });
                }
                _cart.AddToCart(cartForAdd);
            }
            else
            {
                UpdateQuanityListCartExits(cexits.Id, rId);
            }
            return RedirectToPage("/Cart");
        }

        public void UpdateQuanityListCartExits(int cartid, int rid)
        {
            List<ItemDTO> itemlist = _cart.getItemByCartId(cartid);
            if (itemlist != null)
            {
                var listp = _p.GetAllProductByRoomId(rid);
                foreach (var item in itemlist)
                {
                    foreach (var item1 in listp)
                    {
                        if (item.productId == item1.ProductId)
                        {
                            item.quanity++;
                        }
                    }
                }

                _cart.UpdateCartItems(itemlist);
            }
        }


        //public void OnPostUpdateQuantity(int id, int quantity)
        //{
        //    var cart = _cart.getCartByID(id);
        //    if (cart != null)
        //    {
        //        cart.quantity = quantity;
        //        _cart.UpdateCart(id, cart.quantity);
        //    }
        //    OnGet();
        //}

        public void OnPostDeleteCart(int cID)
        {
            var cart = _cart.getCartByID(cID);
            if (cart != null)
            {
                _cart.DeleteCart(cID);
            }
            OnGet();
        }

        //public void OnPostCaculator(int roomAre)
        //{
        //    Are = roomAre;
        //    var carts = _cart.getAllCart();
        //    foreach (var item in carts)
        //    {
        //        var size = (int)_p.GetProductById(item.productId).Size;
        //        var categoryId = (int)_p.GetProductById(item.productId).Category.Id;
        //        item.quantity =  calProduct(roomAre,size, categoryId);
        //        _cart.UpdateCart(item.Id, item.quantity);
        //    }
        //    OnGet();
        //}
        /*public void OnPostCaculator(int roomAre)
        {
            var carts = _cart.getAllCart();
            foreach (var item in carts)
            {
                var size = (int)_p.GetProductById(item.productid).Size;
                var categoryId = (int)_p.GetProductById(item.productId).Categorys.Id;
                item.quantity =  calProduct(roomAre,size, categoryId);
                _cart.UpdateCart(item.Id, item.quantity);
            }
            OnGet();
        }*/

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
                case 4:// ?�n 
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 5;
                    break;
                default:
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 3;
                    break;
            }
            return (int)Math.Ceiling(numberOfProducts);

        }

        public async void OnPostAddQuotation(DateTime createdate , string rdescription, int SelectedOption)
        {
            if(_cart.getAllCart().Count <= 0)
            {
                ViewData["msgcheckcart"] = "Please choose least one product or more than for make quotation.";
            }
            else
            {
                //room
                //room product
                //quotation
                RoomDTO rFA = new RoomDTO();
                rFA.Area = Are;
                rFA.RoomDescription = rdescription;
                bool createRSucsess = await _r.CreateRoom(rFA);
                if (createRSucsess)
                {
                    foreach (var p in _cart.getAllCart())
                    {
                        
                    }
                }

            }
            OnGet();
        }

    }
}
