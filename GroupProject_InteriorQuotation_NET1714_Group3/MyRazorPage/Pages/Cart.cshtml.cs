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

        [BindProperty]
        public List<int> SelectedCartIDs { get; set; }
        public CartModel(ICartService cart, IProductService p, IQuotationService q, IRoomService r, IRoomTypeService rt)
        {
            _cart = cart;
            _p = p;
            _q = q;
            _r = r;
            _rt = rt;
        }
        public List<CartDTO> carts;
        public List<RoomTypeDTO1> roomtypes; 
        public ProductDto getProductById(int productId)
        {
            var p = _p.GetProductByIdToCart(productId);
            return p;
        }
        public Task<ProductDto> getProductById1(int productId)
        {
            var p = _p.GetProductByIdWithAll(productId);
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
                cartForAdd.rAre = 0f;
                cartForAdd.rType = 0;
                cartForAdd.rDescrip = "";
                if (cartForAdd.Items == null)
                {
                    cartForAdd.Items = new List<ItemDTO>();
                }
                int itemid = 1;
                foreach (var item in room)
                {
                    cartForAdd.Items.Add(new ItemDTO
                    {
                        Id = itemid,
                        productId = item.ProductId,
                        cartId = cartForAdd.Id,
                        quanity = 1
                    });
                    itemid++;
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
        public async Task OnPostDeleteCart(int cID, int itemid)
        {
            var cart = _cart.getItemByCartId(cID);
            if (cart != null)
            {
                _cart.DeleteItemInCart(cID, itemid);
            }
            carts = _cart.getAllCart() ?? new List<CartDTO>();
            roomtypes = await _rt.GetAllRoomTypeDTOs();
        }
        public async Task OnPostUpdateQuantity(int cID, int itemid, int quantity)
        {
            var cart = _cart.getItemByCartId(cID);
            if (cart != null)
            {
                foreach (var item in cart)
                {
                    if(item.Id == itemid)
                    {
                        item.quanity = quantity;
                        _cart.UpdateCartItems(cart);
                    }
                }
            }
            carts = _cart.getAllCart() ?? new List<CartDTO>();
            roomtypes = await _rt.GetAllRoomTypeDTOs();
        }
        public async Task OnPostCaculator(int cartID, int roomArea, int SelectedOption, string rDescription)
        {
            var items = _cart.getItemByCartId(cartID);
            foreach (var item in items)
            {
                //var size = (int)_p.GetProductById(item.productId).Size;
                var categoryId = (int)_p.GetProductById(item.productId).Category.Id;
                item.quanity = calProduct(roomArea, categoryId);
                _cart.UpdateCartItems(items);
            }
            carts = _cart.getAllCart() ?? new List<CartDTO>();
            roomtypes = await _rt.GetAllRoomTypeDTOs();
        }
        private int calProduct(int rAre, int category)
        {
            double requiredAreaWithBuffer = rAre;
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
                case 4:// light 
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 5;
                    break;
                case 5:// rug 
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 1;
                    break;
                case 6:// bookshefk 
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 1;
                    break;
                case 7:// table 
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 1;
                    break;
                case 8: // modem  
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 2;
                    break;
                case 9:// Tivi 
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 1;
                    break;
                default:
                    numberOfProducts = (requiredAreaWithBuffer / 20.0) * 3;
                    break;
            }
            return (int)Math.Ceiling(numberOfProducts);

        }
        public IActionResult OnPostAddQuotation([FromQuery] List<string> listcheckbox, DateTime createdate , string rdescription, int SelectedOption)
        {

            List<string> ss = ViewData["listcheckbox"] as List<string>;
            //room
            //room product
            //quotation
            RoomDTO rFA = new RoomDTO();
                rFA.Area = Are;
                rFA.RoomDescription = rdescription;
                //bool createRSucsess = await _r.CreateRoom(rFA);
                //if (createRSucsess)
                //{
                //    foreach (var p in _cart.getAllCart())
                //    {

                //    }
                //}

            return Page();
        }

    }
}
