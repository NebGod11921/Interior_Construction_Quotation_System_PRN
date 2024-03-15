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
            Color = await _colorService.GetAllColors();
            Categories = await _categoryService.GetAllCate();
            Material = await _materialService.GetAllMaterial();
            RoomType = await _roomTypeService.GetAllRoomTypeToAdd();
            Product = new ProductDto
            {
                ProductName = TempData["ProductName"] as string,
                Description = TempData["Description"] as string,
                Quantity = TempData["Quantity"] != null ? Convert.ToInt32(TempData["Quantity"]) : 0,
                Size = TempData["Size"] != null ? Convert.ToInt32(TempData["Size"]) : 0,
                Price = TempData["Price"] != null ? Convert.ToSingle(TempData["Price"]) : 0,
                Color = TempData["ColorId"] as string,
                Material = TempData["MaterialId"] as string,
                RoomTypeId = TempData["RoomTypeId"] != null ? Convert.ToInt32(TempData["RoomTypeId"]) : 0,
                RoomName = TempData["RoomName"] as string,
                Category = TempData["CategoryName"] as string              
            };
            TempData.Keep("ProductName");
            TempData.Keep("Description");
            TempData.Keep("Quantity");
            TempData.Keep("Size");
            TempData.Keep("Price");
            TempData.Keep("ColorId");
            TempData.Keep("MaterialId");
            TempData.Keep("RoomTypeId");
            TempData.Keep("RoomName");
            TempData.Keep("CategoryName");

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
                    TempData["ErrorMessage"] = "All fields need required";
                    return Page();
                }
                else
                {
                    bool isProductNameExists = await _productService.CheckExistProductName(ProductName);
                    if (isProductNameExists)
                    {
                        TempData["ErrorMessage"] = "Product name already exists.";
                        return Page();
                    }
                    if (Request.Form["selectedCategoryId"].Count == 0)
                    {
                        TempData["ErrorMessage"] = "Category must be selected.";
                        return Page();
                    }
                    //var cateid = Convert.ToInt32(Request.Form["selectedCategoryId"]);
                    if (Request.Form["selectedRoomTypeId"].Count == 0)
                    {
                        TempData["ErrorMessage"] = "Room type must be selected.";
                        return Page();
                    }
                    //var roomtypeId = Convert.ToInt32(Request.Form["selectedRoomTypeId"]);
                    if (Request.Form["selectedRoomId"].Count == 0)
                    {
                        TempData["ErrorMessage"] = "Room must be selected.";
                        return Page();
                    }
                    //var roomId = Convert.ToInt32(Request.Form["selectedRoomId"]);
                    if (!int.TryParse(Request.Form["Quantity"], out int quantity) && quantity < 0)
                    {
                        TempData["ErrorMessage"] = "Quantity must be a non-negative integer.";
                        return Page();
                    }

                    if (!int.TryParse(Request.Form["Size"], out int size) && size < 0)
                    {
                        TempData["ErrorMessage"] = "Size must be a non-negative integer.";
                        return Page();
                    }
                    if (!float.TryParse(Request.Form["Price"], out float price) && price < 0)
                    {
                        TempData["ErrorMessage"] = "Price must be a non-negative number.";
                        return Page();
                    }
                    if (imageFile == null || imageFile.Length == null || string.IsNullOrEmpty(imageFile.FileName))
                    {
                        TempData["ErrorMessage"] = "Image file is required.";
                        return Page();
                    }
                    if (Request.Form["selectedColorId"].Count == 0)
                    {
                        TempData["ErrorMessage"] = "Color must be selected.";
                        return Page();
                    }
                    //var clorid = Convert.ToInt32(Request.Form["selectedColorId"]);
                    if (Request.Form["selectedMaterialId"].Count == 0)
                    {
                        TempData["ErrorMessage"] = "Material must be selected.";
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
                var roomTypeName = roomtypename.RoomTypeName;
                var roomidname = _roomService.GetRoomNameByRoomID(roomId);
                var (imagePath, imageid) = await SaveImageAsync(imageFile, roomTypeName);
                if (string.IsNullOrEmpty(imagePath))
                {
                    TempData["ErrorMessage"] = "Fail to save image";
                    return Page();
                }
                var color = await _colorService.GetColorNameById(clorid);
                if (color == null)
                {
                    TempData["ErrorMessage"] = "Invalid color selected";
                    return Page();
                }
                var colorname = color.ColourName;
                var material = await _materialService.GetMaterialById(materid);
                if (material == null)
                {
                    TempData["ErrorMessage"] = "Invalid material selected";
                    return Page();
                }
                var materiala = material.MaterialName;
                var catename = await _categoryService.GetCateNameById(cateid);
                if (catename == null)
                {
                    TempData["ErrorMessage"] = "Invalid catename selected";
                    return Page();
                }
                var catenameee = catename.CateName;
                var productDto = new ProductDto
                {
                    ProductName = Request.Form["ProductName"],
                    RoomTypeId = roomtypeId,
                    Description = Request.Form["Description"],
                    Quantity = Convert.ToInt32(Request.Form["Quantity"]),
                    RoomName = roomidname,
                    RoomId = roomId,
                    RoomTypeName = Convert.ToString(roomTypeName),
                    Size = Convert.ToInt32(Request.Form["Size"]),
                    Price = Convert.ToSingle(Request.Form["Price"]),
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
                            TempData["SuccessMessage"] = "Product add successfully.";
                            return RedirectToPage("/Admin/ProductByAdmin");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Failed to add product";
                            return Page();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to add image.");
                        return Page();
                    }
                }

                else
                {
                    TempData["ErrorMessage"] = "Failed not can be more add product";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return Page();
            }

        }


    }
}
