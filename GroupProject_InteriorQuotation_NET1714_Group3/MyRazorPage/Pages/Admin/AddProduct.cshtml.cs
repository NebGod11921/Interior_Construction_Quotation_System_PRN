using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;

namespace MyRazorPage.Pages.Admin
{
    public class AddProductModel : PageModel
    {
        private IProductService _productService;
        private IColorService _colorService;
        private IMaterialService _materialService;
        private IMapper _mapper;
        private IRoomTypeService _roomTypeService;
        private IRoomService _roomService;
        private ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;
        public AddProductModel(IProductService productService, IColorService colorService, 
            IMaterialService materialService, IMapper mapper, IRoomTypeService roomTypeService,
            IRoomService roomService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment, IImageService imageService)
        {
            _productService = productService;
            _colorService = colorService;
            _materialService = materialService;
            _mapper = mapper;
            _roomTypeService = roomTypeService;
            _roomService = roomService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
        }

        public ProductDto Product { get; set; }
        public List<ColorDTO> Color { get; set; }
        public List<MaterialDTO> Material { get; set; }
        public  List<RoomTypeDTO> RoomType { get; set; }
        public List<RoomHomePageDTO> Rooms { get; set; }
        public List<CategoryDTO> Categories { get; set; } 
        public async Task OnGetAsync()
        {
            if (Color == null)
            {
                Color = await _colorService.GetAllColors();
            }
            if (Material == null)
            {
                Material = await _materialService.GetAllMaterial();
            }
            if (Categories == null)
            {
                Categories = await _categoryService.GetAllCate();
            }
            RoomType = await _roomTypeService.GetAllRoomTypeToAdd();          
        }
        public IActionResult OnGetGetRoomsByRoomType(int roomtypeId)
        {
            var rooms = _roomService.GetRoomsByRoomType(roomtypeId);
            if (rooms == null)
            {
                return NotFound(); 
            }

            return new JsonResult(rooms);
        }
        
        private async Task<(string, int)> SaveImageAsync(IFormFile imageFile, string roomTypeName)
        {

            if (imageFile == null)
            {
                throw new ArgumentNullException(nameof(imageFile), "Image file is null.");
            }
            if (string.IsNullOrEmpty(roomTypeName))
            {
                throw new ArgumentNullException(nameof(roomTypeName), "Room type name is null or empty.");
            }
            var relativePath = $"/Images/{roomTypeName}/{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", roomTypeName);
                      
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, Path.GetFileName(relativePath));           
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }         
            int imageId = await _imageService.AddImageToDatabaseAsync(relativePath);     
           return (relativePath, imageId);
        }

        public async Task<IActionResult> OnPost(string ProductName, IFormFile imageFile, string Description, int Quantity, int Size, float Price)
        {
            try
            {
                var roomtypeId = Convert.ToInt32(Request.Form["selectedRoomTypeId"]);
                var roomId = Convert.ToInt32(Request.Form["selectedRoomId"]);
                var clorid = Convert.ToInt32(Request.Form["selectedColorId"]);
                var materid = Convert.ToInt32(Request.Form["selectedMaterialId"]);
                var cateid = Convert.ToInt32(Request.Form["selectedCategoryId"]);
                if (string.IsNullOrWhiteSpace(ProductName) && string.IsNullOrWhiteSpace(Description))
                {
                    ModelState.AddModelError("", "Product name and description are required.");
                    return Page();

                }
                else
                {
                    bool isProductNameExists = await _productService.CheckExistProductName(ProductName);
                    if (isProductNameExists)
                    {
                        ModelState.AddModelError("", "Product name exist");
                        return Page();
                    }
                    if (Request.Form["selectedCategoryId"].Count == 0)
                    {
                        ModelState.AddModelError("", "selectedCategoryId  are required.");
                        return Page();
                    }
                    //var cateid = Convert.ToInt32(Request.Form["selectedCategoryId"]);
                    if (Request.Form["selectedRoomTypeId"].Count == 0)
                    {
                        ModelState.AddModelError("", "selectedRoomTypeId  are required.");
                        return Page();
                    }
                    //var roomtypeId = Convert.ToInt32(Request.Form["selectedRoomTypeId"]);
                    if (Request.Form["selectedRoomId"].Count == 0)
                    {
                        ModelState.AddModelError("", "selectedRoomId  are required.");
                        return Page();
                    }
                    //var roomId = Convert.ToInt32(Request.Form["selectedRoomId"]);
                    if (!int.TryParse(Request.Form["Product.Quantity"], out int quantity) && quantity < 0)
                    {
                        ModelState.AddModelError("", "selectedRoomId  are required.");
                        return Page();
                    }

                    if (!int.TryParse(Request.Form["Product.Size"], out int size) && size < 0)
                    {
                        ModelState.AddModelError("", "selectedRoomId  are required.");
                        return Page();
                    }
                    if (!float.TryParse(Request.Form["Product.Price"], out float price) && price < 0)
                    {
                        ModelState.AddModelError("", "selectedRoomId  are required.");
                        return Page();
                    }
                    if (imageFile == null || imageFile.Length == null || string.IsNullOrEmpty(imageFile.FileName))
                    {
                        ModelState.AddModelError("", "selectedRoomId  are required.");
                        return Page();
                    }
                    if (Request.Form["selectedColorId"].Count == 0)
                    {
                        ModelState.AddModelError("", "selectedRoomId  are required.");
                        return Page();
                    }
                    //var clorid = Convert.ToInt32(Request.Form["selectedColorId"]);
                    if (Request.Form["selectedMaterialId"].Count == 0)
                    {
                        ModelState.AddModelError("", "selectedRoomId  are required.");
                        return Page();
                    }


                }
                //var materid = Convert.ToInt32(Request.Form["selectedMaterialId"]);
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Error in ModelState.";
                    return Page();
                }
                var roomtypename = await _roomTypeService.GetRoomTypeNameById(roomtypeId);
                if (roomtypename == null)
                {
                    ModelState.AddModelError("roomTypeNameId", "Room type not found.");
                    return Page();
                }
                var catename = await _categoryService.GetCateNameById(cateid);
                if (catename == null)
                {
                    ModelState.AddModelError("", "cate  are faield.");
                    return Page();
                }
                var catenameee = catename.CateName;


                var roomTypeName = roomtypename.RoomTypeName;
                var roomidname = _roomService.GetRoomNameByRoomID(roomId);
                var (imagePath, imageid) = await SaveImageAsync(imageFile, roomTypeName);
                if (string.IsNullOrEmpty(imagePath))
                {
                    ModelState.AddModelError("", "image  are failed.");
                    return Page();
                }
                var color = await _colorService.GetColorNameById(clorid);
                if (color == null)
                {
                    ModelState.AddModelError("", "colro are failed");
                    return Page();
                }
                var colorname = color.ColourName;
                var material = await _materialService.GetMaterialById(materid);
                if (material == null)
                {
                    ModelState.AddModelError("", "material  are faield.");
                    return Page();
                }
                var materiala = material.MaterialName;

                var productDto = new ProductDto
                {
                    ProductName = Request.Form["ProductName"],
                    RoomTypeId = roomtypeId,
                    Description = Request.Form["Description"],
                    Quantity = Convert.ToInt32(Request.Form["Product.Quantity"]),
                    RoomName = roomidname,
                    RoomId = roomId,
                    RoomTypeName = Convert.ToString(roomTypeName),
                    Size = Convert.ToInt32(Request.Form["Product.Size"]),
                    Price = Convert.ToSingle(Request.Form["Product.Price"]),
                    ImageUrl = imagePath,
                    Color = colorname,
                    Material = materiala,
                    Category = catenameee,
                    IsDeleted = true
                };

                var newProductId = await _productService.AddProductAsync(productDto);
                if (newProductId > 0)
                {
                    var productimage = new ProductImage
                    {
                        ProductId = newProductId,
                        ImageId = imageid
                    };
                    bool addimage = await _imageService.AddProductImage(newProductId, imageid);
                    if (addimage)
                    {
                        var roomProduct = new RoomProduct
                        {
                            ProductId = newProductId,
                            RoomId = roomId
                        };
                        bool added = await _productService.AddRoomProductAsync(newProductId, roomId);

                        if (added)
                        {

                            return RedirectToPage("/Admin/ProductByAdmin");
                        }
                        else
                        {
                            ModelState.AddModelError("", "failed add");
                            return Page();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to add image.");
                        return Page();
                    }
                }

                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                return Page();
            }

        }


    }
}
